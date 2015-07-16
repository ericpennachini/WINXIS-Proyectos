using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using TestFacturaElectronica.Dominio.LoginWS;
using System.Runtime.Serialization;

namespace TestFacturaElectronica.Dominio
{
    /// <summary>
    /// Clase encargada de la autorización contra el WSAA
    /// </summary>
    [Serializable]
    public class Autorizacion
    {
        #region Campos y propiedades
        public LoginCMSClient servicioWsaa { get; set; }
        public string TicketAccesoTemplateXml = "<loginTicketRequest>" +
                                                    "<header>" +
                                                        "<uniqueId></uniqueId>" +
                                                        "<generationTime></generationTime>" +
                                                        "<expirationTime></expirationTime>" +
                                                    "</header>" +
                                                    "<service></service>" +
                                                 "</loginTicketRequest>";
        public Int64 UniqueId { get; set; }
        public DateTime GenerationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Service { get; set; }
        public string Token { get; set; }
        public string Sign { get; set; }
        public XmlDocument XmlTicketAccesoRequest { get; set; }
        public XmlDocument XmlTicketAccesoResponse { get; set; }
        public string RutaCertificadoPredef { get; set; }
        public string CmsFirmadoBase64 { get; set; }
        public string UrlServicio { get; set; }

        public static Int32 _globalUniqueID = 1;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de Autorizacion
        /// </summary>
        public Autorizacion()
        {
            RutaCertificadoPredef = @"C:\WSFE - Certificados\";
            servicioWsaa = new LoginCMSClient();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Obtiene el TA (Ticket de Acceso) generando el XML y codificandolo para mandarlo al WSAA, 
        /// e interpreta la respuesta recibida para extraer el Token y Sign
        /// </summary>
        public void ObtenerTicketAcceso(long cuit)
        {
            XmlNode nodoUniqueId;
            XmlNode nodoGenerationTime;
            XmlNode nodoExpirationTime;
            XmlNode nodoService;
            string ticketAccesoResponse;

            #region PASO 1: genero el Ticket request
            try
            {
                this.XmlTicketAccesoRequest = new XmlDocument();
                XmlTicketAccesoRequest.LoadXml(TicketAccesoTemplateXml);

                nodoUniqueId = XmlTicketAccesoRequest.SelectSingleNode("//uniqueId");
                nodoGenerationTime = XmlTicketAccesoRequest.SelectSingleNode("//generationTime");
                nodoExpirationTime = XmlTicketAccesoRequest.SelectSingleNode("//expirationTime");
                nodoService = XmlTicketAccesoRequest.SelectSingleNode("//service");

                nodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
                nodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                nodoExpirationTime.InnerText = DateTime.Now.AddMinutes(10).ToString("s");
                nodoService.InnerText = "wsfe";

                _globalUniqueID += 1;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL GENERAR EL TICKET REQUEST: \n" + ex.Message);
            }
            #endregion

            #region PASO 2: leo archivo del disco y lo codifico en Base64
            try
            {
                X509Certificate2 certificadoEnDisco = new X509Certificate2();
                certificadoEnDisco = LeerCertificadoDeDisco(DevolverRuta(cuit));

                //--- codifico el msje
                Encoding encodeMsg = Encoding.UTF8;
                byte[] mensajeBytes = encodeMsg.GetBytes(XmlTicketAccesoRequest.OuterXml);
                byte[] mensajeCodificado = FirmarMensaje(mensajeBytes, certificadoEnDisco);

                CmsFirmadoBase64 = Convert.ToBase64String(mensajeCodificado);
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

            #region PASO 3: invoco al WSAA
            try
            {
                ticketAccesoResponse = servicioWsaa.loginCms(CmsFirmadoBase64);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL INVOCAR AL WSAA: \n" + ex.Message);
            }
            #endregion

            #region PASO 4: desarmo el XML de respuesta
            try
            {
                XmlTicketAccesoResponse = new XmlDocument();
                XmlTicketAccesoResponse.LoadXml(ticketAccesoResponse);
                this.UniqueId = Int64.Parse(XmlTicketAccesoResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlTicketAccesoResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlTicketAccesoResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Token = XmlTicketAccesoResponse.SelectSingleNode("//token").InnerText;
                this.Sign = XmlTicketAccesoResponse.SelectSingleNode("//sign").InnerText;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL LEER EL XML RESPONSE: \n" + ex.Message);
            }
            #endregion

        }

        #region Metodos extra

        /// <summary>
        /// Lee el certificado p12 en disco desde la ruta pasada por parámetro y devuelve un certificado X509
        /// </summary>
        /// <param name="ruta">Ruta del certificado p12 en disco</param>
        /// <returns>Certificado X509</returns>
        private X509Certificate2 LeerCertificadoDeDisco(string ruta)
        {
            X509Certificate2 certificado = new X509Certificate2();
            try
            {
                certificado.Import(File.ReadAllBytes(ruta));
                return certificado;
            }
            catch (System.Security.SecurityException secEx)
            {
                throw new System.Security.SecurityException("ERROR AL LEER EL CERTIFICADO EN BASE A LA RUTA ESPECIFICADA: \n"
                    + secEx.Message + "Permiso actual: " + secEx.PermissionType);
            }
            catch (FileNotFoundException fnfEx)
            {
                throw new FileNotFoundException("ERROR AL LEER EL CERTIFICADO EN BASE A LA RUTA ESPECIFICADA: \n"
                    + fnfEx.Message
                    + "\n >> Se ha creado las carpetas necesarias pero se requiere un certificado p12");
            }
            catch (DirectoryNotFoundException dnfEx)
            {
                throw new DirectoryNotFoundException("ERROR AL LEER EL CERTIFICADO EN BASE A LA RUTA ESPECIFICADA: \n"
                    + dnfEx.Message
                    + "\n >> Se ha creado las carpetas necesarias pero se requiere un certificado p12");
            }
        }

        /// <summary>
        /// Recibe un nro de CUIT para concatenarlo a la ruta predefinida, comprueba si existe la carpeta
        /// (si no existe la crea) y lee el nombre del archivo (certificado p12) para devolver luego la ruta
        /// completa.
        /// </summary>
        /// <param name="cuit">Nro de CUIT</param>
        /// <returns>Ruta completa del archivo (certificado p12)</returns>
        private string DevolverRuta(long cuit)
        {
            string rutaDevolver;

            string rutaBase = RutaCertificadoPredef + cuit.ToString() + @"\";
            if (!Directory.Exists(rutaBase))
            {
                Directory.CreateDirectory(rutaBase);
            }
            DirectoryInfo infoCarpeta = new DirectoryInfo(rutaBase);
            FileInfo[] infoArchivo = infoCarpeta.GetFiles("*.p12");
            if (infoArchivo.Length > 0)
            {
                return rutaDevolver = infoArchivo[0].FullName;
            }
            else
            {
                return rutaBase;
            }
        }

        /// <summary>
        /// Firma el mensaje (XML)
        /// </summary>
        /// <param name="msjeBytes">XML codificado en bytes</param>
        /// <param name="certFirmante">Certificado X509 obtenido en 'private X509Certificate2 LeerCertificadoDeDisco(string ruta)'</param>
        /// <returns>Mensaje firmado</returns>
        private byte[] FirmarMensaje(byte[] msjeBytes, X509Certificate2 certFirmante)
        {
            ContentInfo contenidoMsje = new ContentInfo(msjeBytes);
            SignedCms cmsFirmado = new SignedCms(contenidoMsje);
            CmsSigner cmsFirmante = new CmsSigner(certFirmante);
            cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;
            cmsFirmado.ComputeSignature(cmsFirmante);
            return cmsFirmado.Encode();
        }
        #endregion

        #endregion
    }
}
