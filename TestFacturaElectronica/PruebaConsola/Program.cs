using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronica.PruebaConsola.localhost;
//using TestFacturaElectronica.Dominio;
//using TestFacturaElectronica.Dominio.FacturaElectronicaWS;

namespace TestFacturaElectronica.PruebaConsola
{
    /// <summary>
    /// Programa en consola para pruebas
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            #region PASO 1: Instanciamos el web service y la clase que maneja la factura
            WebService1SoapClient ws = new WebService1SoapClient();
            ServFactElect servicioFacturacion = new ServFactElect();
            #endregion

            #region PASO 2: Llamamos al método del WS para autorizar con el WSAA
            servicioFacturacion = ws.AutorizarConWSAA("C:\\Users\\Eric\\Desktop\\certificado_clave\\pennachini_prueba_wsass.p12", 20360999301);
            #endregion

            #region PASO 3: Seteamos la cabecera de la factura, desde el objeto que maneja la factura
            #endregion

            #region PASO 4: Seteamos el detalle de la factura, desde el objeto que maneja la factura (solo uno para probar)
            #endregion

            #region PASO 5: Autorizo la factura con el WSFE
            #endregion



            //FECAEResponse response = ws.LeerRespuesta();

        }
    }
}
