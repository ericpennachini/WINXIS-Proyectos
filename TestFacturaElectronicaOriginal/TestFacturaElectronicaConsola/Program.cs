using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronicaOriginalDominio;
using TestFacturaElectronicaOriginalDominio.FacturaElectronicaWS;

namespace TestFacturaElectronicaOriginalConsola
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                ServFactElect servicio = new ServFactElect();
                Console.WriteLine(" __________________________________________________________ ");
                Console.WriteLine("|                                                          |");
                Console.WriteLine("| PRUEBA DE FACTURACIÓN ELECTRÓNICA DE AFIP (HOMOLOGACION) |");
                Console.WriteLine("|__________________________________________________________|");
                Console.WriteLine();

                Console.Write("Ingrese un CUIT: ");
                long cuit = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine();
                //Realizo la autorizacion
                servicio.Autorizar(cuit);
                //Llamo al método que arma el request, que serian los campos de la factura
                servicio.ConfigurarRequest();
                //Llamo al web service mandandole el request con la autorizacion
                servicio.Solicitar();

                //Muestro los resultados
                switch (servicio.Response.FeCabResp.Resultado)
                {
                    case "A":
                        Console.WriteLine("Resultado: " + servicio.Response.FeCabResp.Resultado + " (Aprobado)");
                        break;
                    case "P":
                        Console.WriteLine("Resultado: " + servicio.Response.FeCabResp.Resultado + " (Parcial)");
                        break;
                    case "R":
                        Console.WriteLine("Resultado: " + servicio.Response.FeCabResp.Resultado + " (Rechazado)");
                        break;
                }

                if (servicio.Response.Errors != null)
                {
                    for (int i = 0; i < servicio.Response.Errors.Length; i++)
                    {
                        Console.WriteLine("Error: " + servicio.Response.Errors[i].Code + " -> " + servicio.Response.Errors[i].Msg);
                        Console.WriteLine("-------------");
                    }
                }
                else
                {
                    Console.WriteLine("CAE = " + servicio.Response.FeDetResp[0].CAE + "\n Vencimiento: " + servicio.ConvertirAFormatoFecha(servicio.Response.FeDetResp[0].CAEFchVto));
                    Console.WriteLine(servicio.Response.FeCabResp.PtoVta);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("····· EXCEPCION ·····");
                Console.WriteLine("Ocurrio el siguiente error: \n\n" + ex.Message);
                Console.WriteLine("\n - Tipo de excepcion: " + ex.GetType());
                Console.WriteLine("\n - En: " + ex.TargetSite);
                Console.ReadKey();
            }

        }
    }
}
