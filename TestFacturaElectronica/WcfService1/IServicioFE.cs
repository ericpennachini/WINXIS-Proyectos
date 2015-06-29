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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioFE
    {
        [OperationContract]
        void AutorizarConWSAA(string rutaCertificado, long cuit);

        [OperationContract]
        void ConfeccionarFactura(Factura contratoFactura);

        [OperationContract]
        void AutorizarFactura();

        [OperationContract]
        FECAEResponse LeerRespuesta();
        
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.

    [DataContract]
    public class Factura
    {
        [DataMember]
        public FECAECabRequest Cabecera { get; set; }
        [DataMember]
        public FECAEDetRequest[] Detalle { get; set; }
    }
}
