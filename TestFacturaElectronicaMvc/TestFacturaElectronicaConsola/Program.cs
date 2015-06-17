using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronicaDominio;
using TestFacturaElectronicaDominio.FacturaElectronicaWS;
using TestFacturaElectronicaDominio.LoginWS;

namespace TestFacturaElectronicaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                ServiceSoapClient fServ = new ServiceSoapClient();
                ServFactElect servicio = new ServFactElect();
                FEAuthRequest autorizacion = new FEAuthRequest();
                FECAERequest request = new FECAERequest();
                FECAEResponse response = new FECAEResponse();
                LoginCMSClient login = new LoginCMSClient(); // ver como autenticarse desde el codigo
            
                
                //Instancio algunos campos del response que son compuestos, y tambien sus subcampos
                //Observaciones (solo uno para prueba)
                response.FeDetResp = new FECAEDetResponse[1];
                response.FeDetResp[0] = new FECAEDetResponse();
                response.FeDetResp[0].Observaciones = new Obs[1];
                response.FeDetResp[0].Observaciones[0] = new Obs();
                
                //Errores
                response.Errors = new Err[5];
                for (int i = 0; i < 5; i++)
                {
                    response.Errors[i] = new Err();
                }

                
                //Obtengo la autorizacion
                autorizacion = servicio.Autorizar();
                //Llamo al método que arma el request, que serian los campos de la factura
                request = servicio.ConfigurarRequest();
                //Llamo al web service, y me devuelve la respuesta (response)
                response = fServ.FECAESolicitar(autorizacion, request); //necesita una autorizacion y el request

                //Muestro los resultados
                switch (response.FeCabResp.Resultado)
                {
                    case "A":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Aprobado)");
                        break;
                    case "P":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Parcial)");
                        break;
                    case "R":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Rechazado)");
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
                    Console.WriteLine("CAE = " + response.FeDetResp[0].CAE + "\n Vencimiento: " + servicio.ConvertirAFormatoFecha(response.FeDetResp[0].CAEFchVto));
                    Console.WriteLine(response.FeCabResp.PtoVta);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("····· EXCEPCION ·····");
                Console.WriteLine("Ocurrio el siguiente error: \n\n" + ex.Message);
                Console.WriteLine("\n \n- Tipo de excepcion: " + ex.GetType());
                Console.WriteLine("\n \n- En: " + ex.TargetSite);
                Console.ReadKey();
            }
            
        }
    }
}
