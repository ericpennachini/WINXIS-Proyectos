using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using TestFacturaElectronica.Dominio;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;
using System.Web.Services.Protocols;

namespace TestFacturaElectronica.WebService
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/TestFacturaElectronica/WebService/WebService1", Description = "Descripcion")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    //[System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public WebService1() { }

        [WebMethod]
        public FECAEResponse ObtenerCAE(Factura factura, long cuit)
        {
            try
            {
                ServFactElect _servicioFacturacion = new ServFactElect();
                _servicioFacturacion.Autorizar(cuit);
                _servicioFacturacion.SetCabecera(factura.CantRegistros, factura.PuntoVenta, factura.TipoComprobante);
                foreach (Detalle d in factura.DetalleFactura)
                {
                    _servicioFacturacion.SetDetalle(d.Concepto, d.DocTipo, d.DocNro, d.FechaComp,
                        d.ImporteTotal, d.ImporteTotalConc, d.ImporteNeto, d.ImporteIVA, d.ImporteOpExento, d.ImporteTrib,
                        d.FechaServDesde, d.FechaServHasta, d.FechaVtoPago, d.MonedaId, d.MonedaCotiz,
                        d.CbtesAsoc, d.Tributos, d.Iva, d.Opcionales);
                }
                _servicioFacturacion.SetRequest();
                _servicioFacturacion.Solicitar();
                return _servicioFacturacion.Response;
            }
            catch (SoapException ex)
            {
                //throw new Exception("ERROR: \n" + ex.Message);
                throw new SoapException("ERROR :\n" + ex.Message, SoapException.ClientFaultCode, "",ex);
            } 
        }
        
    }
}