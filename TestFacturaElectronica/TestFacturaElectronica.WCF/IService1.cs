using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TestFacturaElectronica.Dominio;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;
using TestFacturaElectronica.Dominio.LoginWS;

namespace TestFacturaElectronica.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        FECAEResponse ObtenerCAE(Factura factura);
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class Factura
    {
        [DataMember]
        public int CantRegistros { get; set; }
        
        [DataMember]
        public int PuntoVenta { get; set; }
        
        [DataMember]
        public int TipoComprobante { get; set; }
        
        [DataMember]
        public List<Detalle> DetalleFactura { get; set; }
    }

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
