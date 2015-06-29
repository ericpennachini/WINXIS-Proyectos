using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TestFacturaElectronicaDominio.FacturaElectronicaWS;
using TestFacturaElectronicaDominio.LoginWS;

namespace TestFacturaElectronicaDominio
{
    public class ServFactElect
    {
        #region Campos
        private ServiceSoapClient _fServ;
        private FEAuthRequest _autorizacion;
        private FECAERequest _request;
        private FECAEResponse _response;
        private FECAECabRequest _cabecera;
        private FECAEDetRequest[] _detalle;
        private Autorizacion _objAutorizacion;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de ServFactElect
        /// </summary>
        public ServFactElect()
        {
            _fServ = new ServiceSoapClient();
            _autorizacion = new FEAuthRequest();
            _request = new FECAERequest();
            _response = new FECAEResponse();
            _cabecera = new FECAECabRequest();
            _objAutorizacion = new Autorizacion();
            //Instancio algunos campos del response que son compuestos, y tambien sus subcampos (solo uno para prueba)

            _response.FeDetResp = new FECAEDetResponse[1];
            _response.FeDetResp[0] = new FECAEDetResponse();
            //Observaciones
            _response.FeDetResp[0].Observaciones = new Obs[1];
            _response.FeDetResp[0].Observaciones[0] = new Obs();
            //Errores
            _response.Errors = new Err[5];
            for (int i = 0; i < 5; i++)
            {
                _response.Errors[i] = new Err();
            }
        }
        #endregion

        #region Propiedades
        public ServiceSoapClient FServ
        {
            get { return _fServ; }
            set { _fServ = value; }
        }
        public FEAuthRequest Autorizacion
        {
            get { return _autorizacion; }
            set { _autorizacion = value; }
        }
        public FECAERequest Request
        {
            get { return _request; }
            set { _request = value; }
        }
        public FECAEResponse Response
        {
            get { return _response; }
            set { _response = value; }
        }
        public Autorizacion ObjAutorizacion
        {
            get { return _objAutorizacion; }
            set { _objAutorizacion = value; }
        }
        #endregion

        #region Metodos

        #region Autorización

        /// <summary>
        ///     Obtiene el token y sign devuelto por el WSAA, y lo asigna al campo _autorizacion.
        /// </summary>
        /// <param name="cuit">CUIT del contribuyente</param>
        public void Autorizar(long cuit)
        {

            _objAutorizacion.ObtenerTicketAcceso();

            _autorizacion.Token = _objAutorizacion.Token;
            _autorizacion.Sign = _objAutorizacion.Sign;
            _autorizacion.Cuit = cuit;

        }
        #endregion

        /// <summary>
        /// Configura la cabecera de la factura
        /// </summary>
        /// <remarks>Se trabaja con un campo de la clase, porque la cabecera siempre es una sola.</remarks>
        /// <param name="_cantReg">Cantidad de registros del  para la factura</param>
        /// <param name="_ptoVta">Nro de punto de venta</param>
        /// <param name="_cbteTipo">Tipo de comprobante (Ej., 1 = factura 'A')</param>
        /// <returns>Cabecera de la factura, en formato FECAECabRequest</returns>
        public FECAECabRequest SetCabecera(int _cantReg, int _ptoVta, int _cbteTipo)
        {
            _cabecera.CantReg = _cantReg;
            _cabecera.PtoVta = _ptoVta;
            _cabecera.CbteTipo = _cbteTipo; //factura "A"

            return _cabecera;
        }

        /// <summary>
        /// Confecciona el detalle, es decir los campos de la factura, y lo devuelve.
        /// </summary>
        /// <see cref="http://www.afip.gob.ar/fe/documentos/manualdesarrolladorCOMPGv26.pdf"/>
        /// <returns>Detalle en formato FECAEDetRequest</returns>
        public FECAEDetRequest SetDetalle(int _concepto, int _docTipo, long _docNro,
            DateTime _cbteFch, double _impTotal, double _impTotConc, double _impNeto, double _impIVA, double _impOpEx, double _impTrib,
            DateTime _fchServDesde, DateTime _fchServHasta, DateTime _fchVtoPago, string _monId, double _monCotiz,
            CbteAsoc[] _cbtesAsoc, Tributo[] _tributo, AlicIva[] _iva, Opcional[] _opcionales)
        {
            FECAEDetRequest _detalle = new FECAEDetRequest();

            #region Comprobación del ultimo comp. autorizado, se suma +1 al ultimo autorizado
            long nroComp = UltimoCompAutorizado() + 1;
            #endregion

            //Detalle - NO OBLIGATORIO = N
            _detalle.Concepto = _concepto;
            _detalle.DocTipo = _docTipo;
            _detalle.DocNro = _docNro;
            _detalle.CbteDesde = nroComp;
            _detalle.CbteHasta = nroComp;
            #region _detalle.CbteFch = <<fecha en string devuelta por el metodo, en base a la fecha actual>>
            _detalle.CbteFch = ConvertirFechaAString(_cbteFch);
            #endregion
            _detalle.ImpTotal = _impTotal;
            _detalle.ImpTotConc = _impTotConc;
            _detalle.ImpNeto = _impNeto;
            _detalle.ImpIVA = _impIVA;
            _detalle.ImpOpEx = _impOpEx;
            _detalle.ImpTrib = _impTrib;
            _detalle.FchServDesde = ConvertirFechaAString(_fchServDesde); //  N
            _detalle.FchServHasta = ConvertirFechaAString(_fchServHasta); //  N
            _detalle.FchVtoPago = ConvertirFechaAString(_fchVtoPago); //  N
            _detalle.MonId = _monId;
            _detalle.MonCotiz = _monCotiz;
            //_detalle.CbtesAsoc = _cbtesAsoc; //  N
            int i = 0;
            foreach (CbteAsoc c in _cbtesAsoc)
            {
                _detalle.CbtesAsoc[i] = new CbteAsoc();
                _detalle.CbtesAsoc[i] = c;
                i++;
            }
            i = 0;
            //_detalle.Tributos = _tributo; //  N
            foreach (Tributo t in _tributo)
            {
                _detalle.Tributos[i] = new Tributo();
                _detalle.Tributos[i] = t;
                i++;
            }
            i = 0;
            //_detalle.Iva = _iva; //  N
            foreach (AlicIva a in _iva)
            {
                _detalle.Iva[i] = new AlicIva();
                _detalle.Iva[i] = a;
                i++;
            }
            i = 0;
            //_detalle.Opcionales = _opcionales; //  N
            foreach (Opcional o in _opcionales)
            {
                _detalle.Opcionales[i] = new Opcional();
                _detalle.Opcionales[i] = o;
                i++;
            }

            return _detalle;
        }

        /// <summary>
        /// Configura el request con la cabecera y los detalles.
        /// </summary>
        /// <param name="_detalles">Arreglo de detalles</param>
        public void SetRequest(FECAEDetRequest[] _detalles)
        {
            _request.FeCabReq = _cabecera;
            //_request.FeDetReq = _detalles;
            int i = 0;
            foreach (FECAEDetRequest d in _detalles)
            {
                _request.FeDetReq[i] = new FECAEDetRequest();
                _request.FeDetReq[i] = d;
                i++;
            }
        }

        /// <summary>
        /// Solicita al WSFE el CAE, mandando la autorizacion recibida por el WSAA y el objeto FECAERequest (que contiene los campos de la factura), configurado anteriormente por el método 'public void ConfigurarRequest()'
        /// </summary>
        public void Solicitar()
        {
            try
            {
                _response = _fServ.FECAESolicitar(_autorizacion, _request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Métodos extra

        /// <summary>
        /// Convierte una fecha con formato manejado por FECAEDetRequest a un System.DateTime
        /// </summary>
        /// <param name="fechaComprobante">String con la fecha, pero en el formato que maneja el FECAEDetRequest.</param>
        /// <returns>Fecha en formato System.DateTime</returns>
        public DateTime ConvertirAFormatoFecha(string fechaComprobante)
        {
            try
            {
                string _dia, _mes, _anio;
                _anio = fechaComprobante.Substring(0, 4);
                _mes = fechaComprobante.Substring(4, 2);
                _dia = fechaComprobante.Substring(6, 2);
                DateTime _fechaADevolver = new DateTime(Convert.ToInt32(_anio), Convert.ToInt32(_mes), Convert.ToInt32(_dia));

                return _fechaADevolver;
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
        }

        /// <summary>
        /// Convierte un objeto de tipo System.DateTime en un System.String, para que el comprobante de la factura lo pueda manejar
        /// </summary>
        /// <param name="fecha">Fecha de tipo System.DateTime</param>
        /// <returns>Conversion de la fecha DateTime recibida a string para el comprobante</returns>
        public string ConvertirFechaAString(DateTime fecha)
        {
            string _fechaADevolver;
            _fechaADevolver = fecha.Year.ToString();
            _fechaADevolver += (fecha.Month < 10) ? "0" + fecha.Month.ToString() : fecha.Month.ToString(); //si es menor a 10 le agrego un 0 adelante para que sea de dos cifras, sino queda como está.
            _fechaADevolver += (fecha.Day < 10) ? "0" + fecha.Day.ToString() : fecha.Day.ToString(); //idem a anterior

            return _fechaADevolver;
        }

        /// <summary>
        /// Obtiene el ultimo comprobante autorizado por el WSFE
        /// </summary>
        /// <returns>Nro. del último comprobante autorizado</returns>
        public long UltimoCompAutorizado()
        {
            FERecuperaLastCbteResponse ultimoComp = new FERecuperaLastCbteResponse();
            ultimoComp = _fServ.FECompUltimoAutorizado(_autorizacion, 1, 1);
            return ultimoComp.CbteNro;
        }

        #endregion

        #endregion
    }
}
