using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TestFacturaElectronicaDominio;
using TestFacturaElectronicaDominio.FacturaElectronicaWS;

namespace FacturacionElectronicaWCF
{
    public class ServicioFE : IServicioFE
    {
        public ServFactElect ServicioFacturacion { get; set; }
        public Autorizacion ServicioAutorizacion { get; set; }

        /// <summary>
        /// Llama al WSAA (Web Service de Autenticación y Autorización) con el fin de obtener el ticket de acceso
        /// (con Token y Sign) para acceder posteriormente al WSFE (Web Service de Facturación Electrónica).
        /// </summary>
        /// <see cref="http://www.afip.gob.ar/ws/default.asp"/>
        /// <param name="rutaCertificado">Ruta local (en el equipo del cliente) del certificado firmado con clave privada.</param>
        /// <param name="cuit">CUIT del contribuyente</param>
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
        public void ConfeccionarFactura(Factura factura)
        {
            ServicioFacturacion.Request.FeCabReq = factura.Cabecera;
            ServicioFacturacion.Request.FeDetReq = factura.Detalle;
        }

        /// <summary>
        /// Autoriza la factura con el WSFE
        /// </summary>
        public void AutorizarFactura()
        {
            ServicioFacturacion.Solicitar();
        }

        /// <summary>
        /// Devuelve la respuesta del WSFE
        /// </summary>
        /// <returns>Objeto FECAEResponse, el cual contiene la respuesta del WSFE (CAE, errores, obesrvaciones, etc.)</returns>
        public FECAEResponse LeerRespuesta()
        {
            return ServicioFacturacion.Response;
        }
    }

}
