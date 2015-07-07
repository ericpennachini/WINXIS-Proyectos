using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using TestFacturaElectronica.Dominio;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;

namespace TestFacturaElectronica.WebService
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/TestFacturaElectronica/WebService/WebService1")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public WebService1() { }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// Llama al WSAA (Web Service de Autenticación y Autorización) con el fin de obtener el ticket de acceso
        /// (con Token y Sign) para acceder posteriormente al WSFE (Web Service de Facturación Electrónica).
        /// ESTE MÉTODO INICIALIZA LA CLASE ServFactElect
        /// </summary>
        /// <see cref="http://www.afip.gob.ar/ws/default.asp"/>
        /// <param name="rutaCertificado">Ruta local (en el equipo del cliente) del certificado firmado con clave privada.</param>
        /// <param name="cuit">CUIT del contribuyente</param>
        [WebMethod]
        public ServFactElect AutorizarConWSAA(string rutaCertificado, long cuit)
        {
            ServFactElect servicioFacturacion = new ServFactElect();
            Autorizacion servicioAutorizacion = new Autorizacion();
            try
            {
                servicioAutorizacion.RutaCertificado = rutaCertificado;
                servicioFacturacion.ObjAutorizacion = servicioAutorizacion;
                servicioFacturacion.Autorizar(cuit);
                return servicioFacturacion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [WebMethod]
        public ServFactElect ConfeccionarCabecera(int _cantReg, int _ptoVta, int _cbteTipo, ServFactElect _servicioFacturacion)
        {
            _servicioFacturacion.SetCabecera(_cantReg, _ptoVta, _cbteTipo);
            return _servicioFacturacion;
        }

        ///// <summary>
        ///// Confecciona un detalle de factura, se llamará tantas veces como detalles hay.
        ///// </summary>
        ///// <param name="_concepto"></param>
        ///// <param name="_docTipo"></param>
        ///// <param name="_docNro"></param>
        ///// <param name="_cbteFch"></param>
        ///// <param name="_impTotal"></param>
        ///// <param name="_impTotConc"></param>
        ///// <param name="_impNeto"></param>
        ///// <param name="_impIVA"></param>
        ///// <param name="_impOpEx"></param>
        ///// <param name="_impTrib"></param>
        ///// <param name="_fchServDesde"></param>
        ///// <param name="_fchServHasta"></param>
        ///// <param name="_fchVtoPago"></param>
        ///// <param name="_monId"></param>
        ///// <param name="_monCotiz"></param>
        ///// <param name="_cbtesAsoc"></param>
        ///// <param name="_tributo"></param>
        ///// <param name="_iva"></param>
        ///// <param name="_opcionales"></param>
        //[WebMethod]
        //public ServFactElect ConfeccionarDetalle(int _concepto, int _docTipo, long _docNro,
        //    DateTime _cbteFch, double _impTotal, double _impTotConc, double _impNeto, double _impIVA, double _impOpEx, double _impTrib,
        //    DateTime _fchServDesde, DateTime _fchServHasta, DateTime _fchVtoPago, string _monId, double _monCotiz,
        //    CbteAsoc[] _cbtesAsoc, Tributo[] _tributo, AlicIva[] _iva, Opcional[] _opcionales, ServFactElect _servicioFacturacion)
        //{
        //    _servicioFacturacion.SetDetalle(_concepto, _docTipo, _docNro,
        //            _cbteFch, _impTotal, _impTotConc, _impNeto, _impIVA, _impOpEx,
        //            _impTrib, _fchServDesde, _fchServHasta, _fchVtoPago, _monId,
        //            _monCotiz, _cbtesAsoc, _tributo, _iva, _opcionales);
        //    return _servicioFacturacion;
        //}

        /// <summary>
        /// Autoriza la factura con el WSFE
        /// </summary>
        [WebMethod]
        public void AutorizarFactura()
        {
            
        }

        ///// <summary>
        ///// Devuelve la respuesta del WSFE
        ///// </summary>
        ///// <returns>Objeto FECAEResponse, el cual contiene la respuesta del WSFE (CAE, errores, obesrvaciones, etc.)</returns>
        //[WebMethod]
        //public FECAEResponse LeerRespuesta(ServFactElect _servicioFacturacion)
        //{
        //    return _servicioFacturacion.Response;
        //}
    }
}