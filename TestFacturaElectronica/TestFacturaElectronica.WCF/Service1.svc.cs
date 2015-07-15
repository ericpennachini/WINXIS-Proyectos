using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;
using TestFacturaElectronica.Dominio;

namespace TestFacturaElectronica.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    public class Service1 : IService1
    {
        public FECAEResponse ObtenerCAE(Factura factura, long cuit)
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
    }
}
