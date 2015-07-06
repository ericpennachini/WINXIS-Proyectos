using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestFacturaElectronica.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    public class Service1 : IService1
    {
        public Factura Fact { get; set; }

        public Service1()
        {
            Fact = new Factura();
        }

        public void AutorizarConWSAA(string _rutaCertificado, long _cuit)
        {
            try
            {
                Fact.ServicioAutorizacion.RutaCertificado = _rutaCertificado;
                Fact.ServicioFacturacion.ObjAutorizacion = Fact.ServicioAutorizacion;
                Fact.ServicioFacturacion.Autorizar(_cuit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ConfeccionarCabecera(int _cantReg, int _ptoVta, int _cbteTipo)
        {
            Fact.ServicioFacturacion.SetCabecera(_cantReg, _ptoVta, _cbteTipo);
        }

        public void ConfeccionarDetalle(int _concepto, int _docTipo, long _docNro, 
            DateTime _cbteFch, double _impTotal, double _impTotConc, double _impNeto, 
            double _impIVA, double _impOpEx, double _impTrib, DateTime _fchServDesde, 
            DateTime _fchServHasta, DateTime _fchVtoPago, string _monId, double _monCotiz, 
            Dominio.FacturaElectronicaWS.CbteAsoc[] _cbtesAsoc, Dominio.FacturaElectronicaWS.Tributo[] _tributo, 
            Dominio.FacturaElectronicaWS.AlicIva[] _iva, Dominio.FacturaElectronicaWS.Opcional[] _opcionales)
        {
            Fact.ServicioFacturacion.SetDetalle(_concepto, _docTipo, _docNro, _cbteFch, _impTotal, _impTotConc, _impNeto, _impIVA, _impOpEx,
                _impTrib, _fchServDesde, _fchServHasta, _fchVtoPago, _monId, _monCotiz, _cbtesAsoc, _tributo, _iva, _opcionales);
        }

        public void AutorizarFactura()
        {
            Fact.ServicioFacturacion.SetRequest();
            Fact.ServicioFacturacion.Solicitar();
        }

        public Dominio.FacturaElectronicaWS.FECAEResponse LeerRespuesta()
        {
            return Fact.ServicioFacturacion.Response;
        }
    }
}
