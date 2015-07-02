using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PruebaConsola.localhost;
using TestFacturaElectronica.Dominio;

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
            //Creamos un arreglo de strings, para que el WS nos devuelva el Token y Sign obtenidos
            string[] auth = new string[2];
            //Llamamos al método
            auth = ws.AutorizarConWSAA("C:\\Users\\Eric\\Desktop\\certificado_clave\\pennachini_prueba_wsass.p12", 20360999301).ToArray();
            //Asignamos el Token y Sign
            servicioFacturacion.ObjAutorizacion.Token = auth[0];
            servicioFacturacion.ObjAutorizacion.Sign = auth[1];
            #endregion

            #region PASO 3: Seteamos la cabecera de la factura, desde el objeto que maneja la factura 
            servicioFacturacion.SetCabecera(1, 1, 1);
            #endregion

            #region PASO 4: Seteamos el detalle de la factura, desde el objeto que maneja la factura (solo uno para probar)
            servicioFacturacion.SetDetalle(1, 80, 20377033251, new DateTime(2015, 7, 2), 121, 0, 100, 21, 0, 0,
                new DateTime(), new DateTime(), new DateTime(), "PES", 1, null, null, null, null);
            #endregion

            //ws.AutorizarFactura();

            //FECAEResponse response = ws.LeerRespuesta();

        }
    }
}
