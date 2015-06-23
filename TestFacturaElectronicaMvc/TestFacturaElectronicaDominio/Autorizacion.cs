using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using TestFacturaElectronicaDominio.LoginWS;

namespace TestFacturaElectronicaDominio
{
    class Autorizacion
    {

        #region Campos y propiedades
        public LoginCMSService servicioWsaa;
        private string TicketAccesoTemplateXml = "<loginTicketRequest>" + 
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
        public string RutaCertificado { get; set; }
        public string CmsFirmadoBase64 { get; set; }
        public string UrlServicio { get; set; }

        private static Int32 _globalUniqueID = 1;
        #endregion

        /// <summary>
        /// Constructor de Autorizacion
        /// </summary>
        public Autorizacion()
        {
            RutaCertificado = "C:\\Users\\Eric\\Desktop\\certificado_clave\\pennachini_prueba_wsass.p12";
            UrlServicio = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms?wsdl";
            servicioWsaa = new LoginCMSService();
        }

        /// <summary>
        /// Obtiene el TA (Ticket de Acceso) generando el XML y codificandolo para mandarlo al WSAA, e interpreta la respuesta recibida para extraer el Token y Sign
        /// </summary>
        public void ObtenerTicketAcceso()
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
                throw new Exception("ERROR AL GENERAR EL TICKET REQUEST: " + ex.Message);
            }
            #endregion

            #region PASO 2: leo archivo del disco y lo codifico en Base64
            try
            {
                X509Certificate2 certificadoEnDisco = new X509Certificate2();
                certificadoEnDisco = LeerCertificadoDeDisco(RutaCertificado);

                //--- codifico el msje
                Encoding encodeMsg = Encoding.UTF8;
                byte[] mensajeBytes = encodeMsg.GetBytes(XmlTicketAccesoRequest.OuterXml);
                byte[] mensajeCodificado = FirmarMensaje(mensajeBytes, certificadoEnDisco);

                CmsFirmadoBase64 = Convert.ToBase64String(mensajeCodificado);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL FIRMAR EL CERTIFICADO: " + ex.Message);
            }
            #endregion

            #region PASO 3: invoco al WSAA
            try
            {
                //LoginCMSService servicioWsaa = new LoginCMSService();
                servicioWsaa.Url = UrlServicio;
                ticketAccesoResponse = servicioWsaa.loginCms(CmsFirmadoBase64);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL INVOCAR AL WSAA: " + ex.Message);
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
                throw new Exception("ERROR AL LEER EL XML RESPONSE: " + ex.Message);
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
            catch (Exception ex)
            {
                throw new Exception("ERROR AL LEER EL ARCHIVO DEL DISCO: " + ex.Message);
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
    }
}
