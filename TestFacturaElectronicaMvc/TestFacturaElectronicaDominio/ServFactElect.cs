using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronicaDominio.FacturaElectronicaWS;

namespace TestFacturaElectronicaDominio
{
    public class ServFactElect
    {

        public FEAuthRequest Autorizar()
        {

            FEAuthRequest autorizacion = new FEAuthRequest();

            autorizacion.Sign = "lkj34h23k4j5";
            autorizacion.Token = "mnbv43m54nvb635";
            autorizacion.Cuit = 20360999301;
            
            return autorizacion;

        }


        public FECAERequest ConfigurarRequest()
        {
            FECAERequest request = new TestFacturaElectronicaDominio.FacturaElectronicaWS.FECAERequest();
            FECAECabRequest cabecera = new FECAECabRequest();
            FECAEDetRequest detalle = new FECAEDetRequest();
            
            //Cabecera
            cabecera.CantReg = 1;
            cabecera.PtoVta = 1;
            cabecera.CbteTipo = 1; //factura "A"
            //Detalle - NO OBLIGATORIO = N
            detalle.Concepto = 1; //Productos
            detalle.DocTipo = 80; //CUIT
            detalle.DocNro = 20360999301;
            detalle.CbteDesde = 1;
            detalle.CbteHasta = 1;
            detalle.CbteFch = "20150612";
            detalle.ImpTotal = 121;
            detalle.ImpTotConc = 0;
            detalle.ImpNeto = 100;
            detalle.ImpIVA = 21;
            detalle.ImpOpEx = 0;
            detalle.ImpTrib = 0;
            //detalle.FchServDesde = "";  N
            //detalle.FchServHasta = "";  N
            //detalle.FchVtoPago = "";    N
            //FEParamGetTiposMonedasRequest moneda = new FEParamGetTiposMonedasRequest(new FEParamGetTiposMonedasRequestBody());
            detalle.MonId = "PES";
            detalle.MonCotiz = 1;
            detalle.CbtesAsoc = null;
            detalle.Tributos = null;
            //AlicIva Iva = new AlicIva();
            //Iva.Id = 5;
            //Iva.BaseImp = 100;
            //Iva.Importe = 21;
            detalle.Iva[0] = new AlicIva
            {
                Id = 5,
                Importe = 21,
                BaseImp = 100
            };
            
            

            request.FeCabReq = cabecera;
            request.FeDetReq[0] = detalle;

            return request;
        }
    }
}
