using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TestFacturaElectronicaDominio;
using TestFacturaElectronicaDominio.FacturaElectronicaWS;


namespace FacturacionElectronicaWCF
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public ServFactElect ServicioFacturacion { get; set; }
        public Autorizacion ServicioAutorizacion { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        
        /// <summary>
        /// Llama al WSAA (Web Service de Autenticación y Autorización) con el fin de obtener el ticket de acceso
        /// (con Token y Sign) para acceder posteriormente al WSFE (Web Service de Facturación Electrónica).
        /// </summary>
        /// <see cref="http://www.afip.gob.ar/ws/default.asp"/>
        /// <param name="rutaCertificado">Ruta local (en el equipo del cliente) del certificado firmado con clave privada.</param>
        /// <param name="cuit">CUIT del contribuyente</param>
        [WebMethod]
        public void AutorizarConWSAA(string rutaCertificado, long cuit)
        {
            ServicioAutorizacion.RutaCertificado = rutaCertificado;
            ServicioFacturacion.ObjAutorizacion = ServicioAutorizacion;
            ServicioFacturacion.Autorizar(cuit);
        }

        /// <summary>
        /// Confecciona la factura, en base a una factura prearmada que el cliente le proporciona.
        /// Esta factura forma parte del DataContract del servicio.
        /// </summary>
        /// <param name="factura">Factura</param>
        [WebMethod]
        public void ConfeccionarFactura(Factura factura)
        {
            ServicioFacturacion.Request.FeCabReq = factura.Cabecera;
            ServicioFacturacion.Request.FeDetReq = factura.Detalle;
        }

        /// <summary>
        /// Autoriza la factura con el WSFE
        /// </summary>
        [WebMethod]
        public void AutorizarFactura()
        {
            ServicioFacturacion.Solicitar();
        }

        /// <summary>
        /// Devuelve la respuesta del WSFE
        /// </summary>
        /// <returns>Objeto FECAEResponse, el cual contiene la respuesta del WSFE (CAE, errores, obesrvaciones, etc.)</returns>
        [WebMethod]
        public FECAEResponse LeerRespuesta()
        {
            return ServicioFacturacion.Response;
        }
    }
}
