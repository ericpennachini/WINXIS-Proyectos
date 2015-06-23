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
            _objAutorizacion = new Autorizacion();

            //Instancio algunos campos del response que son compuestos, y tambien sus subcampos
            _response.FeDetResp = new FECAEDetResponse[1];
            _response.FeDetResp[0] = new FECAEDetResponse();
            //Observaciones (solo uno para prueba)
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
        #endregion

        #region Metodos
        #region Metodos de autorización viejos
        //public void Autorizar(long cuit) //opcion -1-, con algoritmo raro
        //{
        //    ///Creao una instancia dinámica (¿?¿?... Averiguar) de WSAA
        //    ///Aparentemente, busca el archivo wsaa.exe ubicado en C:\Program files (x86)\PyAfipWs
        //    ///en base a la entrada en el registro de Windows.
        //    dynamic WSAA = Activator.CreateInstance(Type.GetTypeFromProgID("WSAA"));
        //    //Console.WriteLine(WSAA.GetType());
        //    //Cargo las rutas del certificado y clave privada, creo el TRA y luego se firma junto con el cert. y la clave privada
        //    string _certificado, _clavePrivada, cms, tra;
        //    _certificado = "C:\\Program Files (x86)\\PyAfipWs\\pennachini_prueba_wsass.crt";
        //    _clavePrivada = "C:\\Program Files (x86)\\PyAfipWs\\pennachini_prueba_wsass.key";
        //    tra = WSAA.CreateTRA("wsfe");
        //    cms = WSAA.SignTRA(tra, _certificado, _clavePrivada);

        //    //se conecta al WSAA y obtiene el TA (Ticket de Acceso) con el token y sign
        //    string proxy = "";
        //    string cache = "";
        //    string wsdl = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms?wsdl";
        //    WSAA.Conectar(cache, wsdl, proxy); // Homologación
        //    string ta = WSAA.LoginCMS(cms);

        //    _autorizacion.Token = WSAA.Token;
        //    _autorizacion.Sign = WSAA.Sign;
        //    _autorizacion.Cuit = cuit;
        //}

        //public void Autorizar(long cuit) //opcion -2-, con el token y sign hardcodeados, usando lo que me da la app visual wsaa
        //{
        //    _autorizacion.Token = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgdW5pcXVlX2lkPSIzODI0NzgwMTUxIiBzcmM9IkNOPXdzYWFob21vLCBPPUFGSVAsIEM9QVIsIFNFUklBTE5VTUJFUj1DVUlUIDMzNjkzNDUwMjM5IiBnZW5fdGltZT0iMTQzNDYzMjgwNSIgZXhwX3RpbWU9IjE0MzQ2NzYwNjUiIGRzdD0iQ049d3NmZSwgTz1BRklQLCBDPUFSIi8+CiAgICA8b3BlcmF0aW9uIHZhbHVlPSJncmFudGVkIiB0eXBlPSJsb2dpbiI+CiAgICAgICAgPGxvZ2luIHVpZD0iQz1hciwgTz1wZW5uYWNoaW5pIGVyaWMgZGFuaWVsLCBTRVJJQUxOVU1CRVI9Q1VJVCAyMDM2MDk5OTMwMSwgQ049cHlhZmlwd3MiIHNlcnZpY2U9IndzZmUiIHJlZ21ldGhvZD0iMjIiIGVudGl0eT0iMzM2OTM0NTAyMzkiIGF1dGhtZXRob2Q9ImNtcyI+CiAgICAgICAgICAgIDxyZWxhdGlvbnM+CiAgICAgICAgICAgICAgICA8cmVsYXRpb24gcmVsdHlwZT0iNCIga2V5PSIyMDM2MDk5OTMwMSIvPgogICAgICAgICAgICA8L3JlbGF0aW9ucz4KICAgICAgICA8L2xvZ2luPgogICAgPC9vcGVyYXRpb24+Cjwvc3NvPgoK";
        //    _autorizacion.Sign = "V78ihkBSpcpQooKkV89odJzECM0or01QChM2DagOAnXtl7qxDOlZCxK/L/3amdPEG5Lp3iGoDQnZkkiVcxXuk4KxEF3UvhbScVbXK5dibLUSu1COE4Dhn6mUoFYWAvIldcj13aL+5P+nCHvd2DD374wR5f3VnajTbka7tmcKw7s=";
        //    _autorizacion.Cuit = cuit;
        //} 
        #endregion

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
        /// <summary>
        /// Configura campo por campo el request que se manda al WSFE
        /// </summary>
        public void ConfigurarRequest() //refactorizar con parámetros, para no hardcodear todo
        {
            FECAECabRequest _cabecera = new FECAECabRequest();
            FECAEDetRequest _detalle = new FECAEDetRequest();

            //Cabecera
            _cabecera.CantReg = 1;
            _cabecera.PtoVta = 1;
            _cabecera.CbteTipo = 1; //factura "A"
            //Detalle - NO OBLIGATORIO = N
            _detalle.Concepto = 1; //Productos
            _detalle.DocTipo = 80; //CUIT
            _detalle.DocNro = 20377033251;
            _detalle.CbteDesde = 18;
            _detalle.CbteHasta = 18;
            //DateTime cbteFch = new DateTime(); //REALIZAR  METODO PARA CONVERTIR FECHA NORMAL A STRING QUE SE USA ACA.
            _detalle.CbteFch = "20150618";
            _detalle.ImpTotal = 121;
            _detalle.ImpTotConc = 0;
            _detalle.ImpNeto = 100;
            _detalle.ImpIVA = 21;
            _detalle.ImpOpEx = 0;
            _detalle.ImpTrib = 0;
            //detalle.FchServDesde = "";  N
            //detalle.FchServHasta = "";  N
            //detalle.FchVtoPago = "";    N
            _detalle.MonId = "PES";
            _detalle.MonCotiz = 1;
            _detalle.CbtesAsoc = null;
            _detalle.Tributos = null;
            _detalle.Iva = new AlicIva[1]; //solo uno a modo de ejemplo

            _detalle.Iva[0] = new AlicIva
            {
                Id = 5,
                Importe = 21,
                BaseImp = 100
            };

            _request.FeCabReq = _cabecera;
            _request.FeDetReq = new FECAEDetRequest[1];
            _request.FeDetReq[0] = new FECAEDetRequest();
            _request.FeDetReq[0] = _detalle;
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

        /// <summary>
        /// Metodo que convierte una fecha con formato manejado por FECAEDetRequest a un System.DateTime
        /// </summary>
        /// <param name="fechaComprobante">String con la fecha, pero en el formato que maneja el FECAEDetRequest.</param>
        /// <returns>Fecha en formato System.DateTime</returns>
        public DateTime ConvertirAFormatoFecha(string fechaComprobante)
        {
            string _dia, _mes, _anio;
            _anio = fechaComprobante.Substring(0, 4);
            _mes = fechaComprobante.Substring(4, 2);
            _dia = fechaComprobante.Substring(6, 2);
            DateTime _fechaADevolver = new DateTime(Convert.ToInt32(_anio), Convert.ToInt32(_mes), Convert.ToInt32(_dia));

            return _fechaADevolver;
        }

        #endregion
    }
}
