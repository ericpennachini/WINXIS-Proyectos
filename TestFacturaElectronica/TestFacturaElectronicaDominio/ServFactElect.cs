using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TestFacturaElectronica.Dominio.FacturaElectronicaWS;
using TestFacturaElectronica.Dominio.LoginWS;
using System.Runtime.Serialization;

namespace TestFacturaElectronica.Dominio
{
    /// <summary>
    /// Clase encargada de manejar la facturación:
    ///     1) Setea los campos, 
    ///     2) autoriza usando la clase Autorización (encargada de manejar la autorización con el web service WSAA), 
    ///     3) solicita la autorización de la factura en espera del CAE (o errores en su defecto) al web service WSFE. 
    /// </summary>
    [Serializable]
    public class ServFactElect
    {
        #region Propiedades
        public ServiceSoapClient FServ { get; set; }
        public FEAuthRequest Autorizacion { get; set; }
        public FECAERequest Request { get; set; }
        public FECAEResponse Response { get; set; }
        public FECAECabRequest CabeceraFactura { get; set; }
        public List<FECAEDetRequest> DetalleFactura { get; set; }
        public Autorizacion ObjAutorizacion { get; set; }
        public int UltimoElementoArrayDetalle { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de ServFactElect
        /// </summary>
        public ServFactElect()
        {
            FServ = new ServiceSoapClient();
            Autorizacion = new FEAuthRequest();
            Request = new FECAERequest();
            Response = new FECAEResponse();
            CabeceraFactura = new FECAECabRequest();
            DetalleFactura = new List<FECAEDetRequest>();
            ObjAutorizacion = new Autorizacion();
        }
        #endregion

        #region Metodos

        /// <summary>
        ///     Obtiene el token y sign devuelto por el WSAA, y lo asigna al campo _autorizacion.
        /// </summary>
        /// <param name="cuit">CUIT del contribuyente</param>
        public void Autorizar(long cuit)
        {
            ObjAutorizacion.ObtenerTicketAcceso();
            Autorizacion.Token = ObjAutorizacion.Token;
            Autorizacion.Sign = ObjAutorizacion.Sign;
            Autorizacion.Cuit = cuit;
        }

        /// <summary>
        /// Configura la cabecera de la factura
        /// </summary>
        /// <remarks>Se trabaja con un campo de la clase, porque la cabecera siempre es una sola.</remarks>
        /// <param name="_cantReg">Cantidad de registros del  para la factura</param>
        /// <param name="_ptoVta">Nro de punto de venta</param>
        /// <param name="_cbteTipo">Tipo de comprobante (Ej., 1 = factura 'A')</param>
        /// <returns>Cabecera de la factura, en formato FECAECabRequest</returns>
        public void SetCabecera(int _cantReg, int _ptoVta, int _cbteTipo)
        {
            CabeceraFactura.CantReg = _cantReg;
            CabeceraFactura.PtoVta = _ptoVta;
            CabeceraFactura.CbteTipo = _cbteTipo;
        }

        /// <summary>
        /// Confecciona el detalle, es decir los campos de la factura, y lo devuelve.
        /// </summary>
        /// <see cref="http://www.afip.gob.ar/fe/documentos/manualdesarrolladorCOMPGv26.pdf"/>
        public void SetDetalle(int _concepto, int _docTipo, long _docNro,
            DateTime _cbteFch, double _impTotal, double _impTotConc, double _impNeto, double _impIVA, double _impOpEx, double _impTrib,
            DateTime _fchServDesde, DateTime _fchServHasta, DateTime _fchVtoPago, string _monId, double _monCotiz,
            List<CbteAsoc> _cbtesAsoc, List<Tributo> _tributo, List<AlicIva> _iva, List<Opcional> _opcionales)
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
            _detalle.CbtesAsoc = (_cbtesAsoc.Count == 0) ? null : _cbtesAsoc; //  N
            _detalle.Tributos = (_tributo.Count == 0) ? null : _tributo; //  N
            _detalle.Iva = (_iva.Count == 0) ? null : _iva; //  N
            _detalle.Opcionales = (_opcionales.Count == 0) ? null : _opcionales; //  N

            DetalleFactura.Add(_detalle);
        }

        /// <summary>
        /// Configura el request con la cabecera y los detalles.
        /// </summary>
        public void SetRequest()
        {
            Request.FeCabReq = CabeceraFactura;
            Request.FeDetReq = DetalleFactura;
        }

        /// <summary>
        /// Solicita al WSFE el CAE, mandando la autorizacion recibida por el WSAA y el objeto FECAERequest (que contiene los campos de la factura), configurado anteriormente por el método 'public void ConfigurarRequest()'
        /// </summary>
        public void Solicitar()
        {
            try
            {
                Response = FServ.FECAESolicitar(Autorizacion, Request);
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
        public static DateTime ConvertirAFormatoFecha(string fechaComprobante)
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
        public static string ConvertirFechaAString(DateTime fecha)
        {
            string _fechaADevolver;

            if ((fecha.Year == 1) && (fecha.Month == 1) && (fecha.Day == 1))
            {
                _fechaADevolver = "";
            }
            else
            {
                _fechaADevolver = fecha.Year.ToString();
                _fechaADevolver += (fecha.Month < 10) ? "0" + fecha.Month.ToString() : fecha.Month.ToString(); //si es menor a 10 le agrego un 0 adelante para que sea de dos cifras, sino queda como está.
                _fechaADevolver += (fecha.Day < 10) ? "0" + fecha.Day.ToString() : fecha.Day.ToString(); //idem a anterior
            }

            return _fechaADevolver;
        }

        /// <summary>
        /// Obtiene el ultimo comprobante autorizado por el WSFE
        /// </summary>
        /// <returns>Nro. del último comprobante autorizado</returns>
        public long UltimoCompAutorizado()
        {
            FERecuperaLastCbteResponse ultimoComp = new FERecuperaLastCbteResponse();
            ultimoComp = FServ.FECompUltimoAutorizado(Autorizacion, 1, 1);
            return ultimoComp.CbteNro;
        }

        #endregion

        #endregion

    }
}
