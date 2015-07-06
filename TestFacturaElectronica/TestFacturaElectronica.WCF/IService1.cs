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
        void AutorizarConWSAA(string _rutaCertificado, long _cuit);

        [OperationContract]
        void ConfeccionarCabecera(int _cantReg, int _ptoVta, int _cbteTipo);

        [OperationContract]
        void ConfeccionarDetalle(int _concepto, int _docTipo, long _docNro,
            DateTime _cbteFch, double _impTotal, double _impTotConc, double _impNeto, double _impIVA, double _impOpEx, double _impTrib,
            DateTime _fchServDesde, DateTime _fchServHasta, DateTime _fchVtoPago, string _monId, double _monCotiz,
            CbteAsoc[] _cbtesAsoc, Tributo[] _tributo, AlicIva[] _iva, Opcional[] _opcionales);

        [OperationContract]
        void AutorizarFactura();

        [OperationContract]
        FECAEResponse LeerRespuesta();


        // TODO: agregue aquí sus operaciones de servicio
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class Factura
    {
        private ServFactElect servicioFacturacion;
        private Autorizacion servicioAutorizacion;
        public Factura()
        {
            servicioFacturacion = new ServFactElect();
            servicioAutorizacion = new Autorizacion();
        }

        [DataMember]
        public ServFactElect ServicioFacturacion
        {
            get { return servicioFacturacion; }
            set { servicioFacturacion = value; }
        }

        [DataMember]
        public Autorizacion ServicioAutorizacion
        {
            get { return servicioAutorizacion; }
            set { servicioAutorizacion = value; }
        }
    }
}
