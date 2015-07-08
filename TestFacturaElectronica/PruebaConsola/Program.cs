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
            try
            {
                #region PASO 1: Instanciamos el web service y la clase que maneja la factura
                WebService1SoapClient ws = new WebService1SoapClient();
                ServFactElect servicioFacturacion = new ServFactElect();
                #endregion

                #region PASO 2: Llamamos al método del WS para autorizar con el WSAA
                Console.Write("Ingrese un nº de CUIT: ");
                long cuit = Convert.ToInt64(Console.ReadLine());
                servicioFacturacion = ws.AutorizarConWSAA("D:\\MIS COSAS\\TRABAJO\\certificado_clave\\pennachini_prueba_wsass.p12", cuit);
                #endregion

                #region PASO 3: Seteamos la cabecera de la factura, desde el objeto que maneja la factura
                servicioFacturacion = ws.ConfeccionarCabecera(1, 1, 1, servicioFacturacion);
                #endregion

                #region PASO 4: Seteamos el detalle de la factura, desde el objeto que maneja la factura (solo uno para probar)
                AlicIva[] iva = new AlicIva[1];
                iva[0] = new AlicIva { Id = 5, Importe = 25, BaseImp = 100 };
                servicioFacturacion = ws.ConfeccionarDetalle(1, 80, 20377033251, new DateTime(2015, 7, 8), 121, 0, 100, 21, 0, 0,
                        new DateTime(), new DateTime(), new DateTime(), "PES", 1, null, null, iva, null, servicioFacturacion);
                #endregion

                #region PASO 5: Autorizo la factura con el WSFE
                servicioFacturacion = ws.AutorizarFactura(servicioFacturacion);
                #endregion

                #region PASO 6: Obtengo el response con el objetivo de ver los campos que me interesen (errores, observaciones, ...)
                FECAEResponse response = new FECAEResponse();
                response = ws.LeerRespuesta(servicioFacturacion);
                #endregion

                #region PASO 7: Muestro por pantalla los errores
                switch (response.FeCabResp.Resultado)
                {
                    case "A":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Aprobado)");
                        Console.ReadKey();
                        break;
                    case "P":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Parcial)");
                        Console.ReadKey();
                        break;
                    case "R":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Rechazado)");
                        Console.ReadKey();
                        break;
                }

                if (response.Errors != null)
                {
                    for (int i = 0; i < response.Errors.Length; i++)
                    {
                        Console.WriteLine("Error: " + response.Errors[i].Code + " -> " + response.Errors[i].Msg);
                        Console.WriteLine("-------------");
                    }
                }
                else
                {
                    Console.WriteLine("CAE = " + response.FeDetResp[0].CAE + "\n Vencimiento: " + response.FeDetResp[0].CAEFchVto);
                }
                Console.ReadKey();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error. ");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("--------------------------------------------");
                Console.ReadKey();
            }

        }
    }
}
