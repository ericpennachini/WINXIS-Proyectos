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
        private string TicketAccesoTemplateXml = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";

        #region Campos y propiedades
        public LoginCMSService servicioWsaa;
        public Int32 UniqueId { get; set; }
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

        private static Int32 _globalUniqueID = 0;
        #endregion

        public Autorizacion()
        {
            RutaCertificado = "C:\\Users\\Eric\\Desktop\\certificado_clave\\pennachini_prueba_wsass.p12";
            servicioWsaa = new LoginCMSService();
        }

        public void ObtenerTicketAcceso()
        {

            XmlNode nodoUniqueId;
            XmlNode nodoGenerationTime;
            XmlNode nodoExpirationTime;
            XmlNode nodoService;
            string ticketAccesoResponse;

            #region PASO 1: Genero el Ticket request
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
                nodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                nodoService.InnerText = servicioWsaa.Url;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL GENERAR EL TICKET REQUEST: " + ex.Message);
            }
            #endregion

            #region PASO 2: Leo archivo del disco
            try
            {
                X509Certificate2 certificadoEnDisco = new X509Certificate2();
                certificadoEnDisco = LeerCertificadoDeDisco(RutaCertificado);

                //--- codifico el msje
                Encoding EncodeMsg = Encoding.UTF8;
                byte[] mensajeBytes = EncodeMsg.GetBytes(XmlTicketAccesoRequest.OuterXml);
                byte[] mensajeCodificado = FirmarMensaje(mensajeBytes, certificadoEnDisco);

                CmsFirmadoBase64 = Convert.ToBase64String(mensajeCodificado);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL FIRMAR EL CERTIFICADO: " + ex.Message);
            }
            #endregion

            #region PASO 3: Invoco al WSAA
            try
            {
                ticketAccesoResponse = servicioWsaa.loginCms(CmsFirmadoBase64);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR AL INVOCAR AL WSAA: " + ex.Message);
            }
            #endregion

            #region PASO 4: desarmo el xml de respuesta
            try
            {
                XmlTicketAccesoResponse = new XmlDocument();
                XmlTicketAccesoResponse.LoadXml(ticketAccesoResponse);
                this.UniqueId = Int32.Parse(XmlTicketAccesoResponse.SelectSingleNode("//uniqueId").InnerText);
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
        public X509Certificate2 LeerCertificadoDeDisco(string ruta)
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

        public byte[] FirmarMensaje(byte[] msjeBytes, X509Certificate2 certFirmante)
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
