using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;

namespace TestFacturaElectronica.WebService
{
    [Serializable]
    public class Factura
    {
        public int CantRegistros { get; set; }
        public int PuntoVenta { get; set; }
        public int TipoComprobante { get; set; }
        public List<Detalle> DetalleFactura { get; set; }
    }

    [Serializable]
    public class Detalle
    {
        public int Concepto { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public DateTime FechaComp { get; set; }
        public double ImporteTotal { get; set; }
        public double ImporteTotalConc { get; set; }
        public double ImporteNeto { get; set; }
        public double ImporteIVA { get; set; }
        public double ImporteOpExento { get; set; }
        public double ImporteTrib { get; set; }
        public DateTime FechaServDesde { get; set; }
        public DateTime FechaServHasta { get; set; }
        public DateTime FechaVtoPago { get; set; }
        public string MonedaId { get; set; }
        public double MonedaCotiz { get; set; }
        public List<CbteAsoc> CbtesAsoc { get; set; }
        public List<Tributo> Tributos { get; set; }
        public List<AlicIva> Iva { get; set; }
        public List<Opcional> Opcionales { get; set; }
    }
}
