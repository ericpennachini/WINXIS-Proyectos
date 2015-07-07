using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronica.PruebaConsola2.FactElectWCF;

namespace TestFacturaElectronica.PruebaConsola2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Service1Client serv = new Service1Client();

                Console.Write("Ingrese un nº de CUIT: ");
                long cuit = Convert.ToInt64(Console.ReadLine());
                serv.AutorizarConWSAA("C:\\Users\\Eric\\Desktop\\certificado_clave\\pennachini_prueba_wsass.p12", cuit);

                serv.ConfeccionarCabecera(1, 1, 1);

                AlicIva[] iva = new AlicIva[1];
                iva[0] = new AlicIva { idField = 5, importeField = 21, baseImpField = 100 };
                serv.ConfeccionarDetalle(1, 80, 20377033251, new DateTime(2015, 7, 6), 121, 0, 100, 21, 0, 0,
                    new DateTime(), new DateTime(), new DateTime(), "PES", 1, null, null, iva, null);

                serv.AutorizarFactura();

                FECAEResponse response = new FECAEResponse();
                response = serv.LeerRespuesta();

                switch (response.feCabRespField.resultadoField)
                {
                    case "A":
                        Console.WriteLine("Resultado: " + response.feCabRespField.resultadoField + " (Aprobado)");
                        Console.ReadKey();
                        break;
                    case "P":
                        Console.WriteLine("Resultado: " + response.feCabRespField.resultadoField + " (Parcial)");
                        Console.ReadKey();
                        break;
                    case "R":
                        Console.WriteLine("Resultado: " + response.feCabRespField.resultadoField + " (Rechazado)");
                        Console.ReadKey();
                        break;
                }

                if (response.errorsField != null)
                {
                    for (int i = 0; i < response.errorsField.Length; i++)
                    {
                        Console.WriteLine("Error: " + response.errorsField[i].codeField + " -> " + response.errorsField[i].msgField);
                        Console.WriteLine("-------------");
                    }
                }
                else
                {
                    Console.WriteLine("CAE = " + response.feDetRespField[0].cAEField + "\n Vencimiento: " + response.feDetRespField[0].cAEFchVtoField);
                }
                Console.ReadKey();
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
