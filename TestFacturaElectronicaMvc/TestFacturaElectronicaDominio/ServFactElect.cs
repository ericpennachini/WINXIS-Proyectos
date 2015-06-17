using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronicaDominio.FacturaElectronicaWS;
using TestFacturaElectronicaDominio.LoginWS;

namespace TestFacturaElectronicaDominio
{
    public class ServFactElect
    {
        //ServiceSoapClient fServ = new ServiceSoapClient();
        
        public FEAuthRequest Autorizar()
        {

            FEAuthRequest _autorizacion = new FEAuthRequest();

            _autorizacion.Token = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgdW5pcXVlX2lkPSIxMzI3MTk1MDgwIiBzcmM9IkNOPXdzYWFob21vLCBPPUFGSVAsIEM9QVIsIFNFUklBTE5VTUJFUj1DVUlUIDMzNjkzNDUwMjM5IiBnZW5fdGltZT0iMTQzNDU0MTgyNiIgZXhwX3RpbWU9IjE0MzQ1ODUwODYiIGRzdD0iQ049d3NmZSwgTz1BRklQLCBDPUFSIi8+CiAgICA8b3BlcmF0aW9uIHZhbHVlPSJncmFudGVkIiB0eXBlPSJsb2dpbiI+CiAgICAgICAgPGxvZ2luIHVpZD0iQz1hciwgTz1wZW5uYWNoaW5pIGVyaWMgZGFuaWVsLCBTRVJJQUxOVU1CRVI9Q1VJVCAyMDM2MDk5OTMwMSwgQ049cHlhZmlwd3MiIHNlcnZpY2U9IndzZmUiIHJlZ21ldGhvZD0iMjIiIGVudGl0eT0iMzM2OTM0NTAyMzkiIGF1dGhtZXRob2Q9ImNtcyI+CiAgICAgICAgICAgIDxyZWxhdGlvbnM+CiAgICAgICAgICAgICAgICA8cmVsYXRpb24gcmVsdHlwZT0iNCIga2V5PSIyMDM2MDk5OTMwMSIvPgogICAgICAgICAgICA8L3JlbGF0aW9ucz4KICAgICAgICA8L2xvZ2luPgogICAgPC9vcGVyYXRpb24+Cjwvc3NvPgoK";
            _autorizacion.Sign = "KJGlK4vxSxHxkbqLeOyaIUSVBk/MYBg5ouxIAsWWsZjqZljQNMvWvxSP2UFp2DBRmtiWO3U1sfcfBd1+xhFNa6uRLA84uc20b90wbZR5N8wy74pFEPHuhdhyug35k2Huzzwtpg+s501KRvhBWzG6b40/bfO8OdRQODcOo5BY/c4=";
            _autorizacion.Cuit = 20360999301;
            
            return _autorizacion;

        }

        public FECAERequest ConfigurarRequest()
        {
            FECAERequest _request = new TestFacturaElectronicaDominio.FacturaElectronicaWS.FECAERequest();
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
            _detalle.CbteDesde = 13;
            _detalle.CbteHasta = 13;
            _detalle.CbteFch = "20150617";
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

            return _request;
        }

        //public FECAEResponse Solicitar()
        //{
        //}    
        
        public DateTime ConvertirAFormatoFecha(string fechaComprobante)
        {
            string _dia, _mes, _anio;
            _anio = fechaComprobante.Substring(0, 4);
            _mes = fechaComprobante.Substring(4, 2);
            _dia = fechaComprobante.Substring(6, 2);
            DateTime _fechaADevolver = new DateTime(Convert.ToInt32(_anio), Convert.ToInt32(_mes), Convert.ToInt32(_dia));
            
            return _fechaADevolver;
        }
    }
}
