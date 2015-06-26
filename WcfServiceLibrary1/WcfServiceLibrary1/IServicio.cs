using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TestFacturaElectronicaDominio;

namespace WcfServiceLibrary1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicio" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicio
    {
        
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio
    [DataContract]
    public class CompositeType
    {
        private ServFactElect _servicioFacturacion;
        private Autorizacion _autorizacion;

        [DataMember]
        public Autorizacion Autorizacion
        {
            get { return _autorizacion; }
            set { _autorizacion = value; }
        }

        [DataMember]
        public ServFactElect ServicioFacturacion
        {
            get { return _servicioFacturacion; }
            set { _servicioFacturacion = value; }
        }

    }
}
