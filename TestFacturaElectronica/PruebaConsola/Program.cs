using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronica.PruebaConsola.localhost;

namespace TestFacturaElectronica.PruebaConsola
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Ingrese CUIT: ");
                long cuit = Convert.ToInt64(Console.ReadLine());

                WebService1SoapClient ws = new WebService1SoapClient();
                Factura factura = new Factura();
                Detalle detalle = new Detalle();
                Detalle detalle2 = new Detalle();

                #region Completando la factura (para probar, 2 detalles)...
                factura.CantRegistros = 2;
                factura.PuntoVenta = 1;
                factura.TipoComprobante = 1;

                #region Completando detalle 1...
                detalle.Concepto = 1;
                detalle.DocTipo = 80;
                detalle.DocNro = 20377033251;
                detalle.FechaComp = new DateTime(2015, 7, 14);
                detalle.ImporteTotal = 121;
                detalle.ImporteTotalConc = 0;
                detalle.ImporteNeto = 100;
                detalle.ImporteIVA = 21;
                detalle.ImporteOpExento = 0;
                detalle.ImporteTrib = 0;
                detalle.FechaServDesde = new DateTime();
                detalle.FechaServHasta = new DateTime();
                detalle.FechaVtoPago = new DateTime();
                detalle.MonedaId = "PES";
                detalle.MonedaCotiz = 1;
                //detalle.CbtesAsoc --> no agregamos nada
                detalle.CbtesAsoc = null;
                //detalle.Tributos --> no agregamos nada
                detalle.Tributos = null;
                //detalle.Opcionales --> no agregamos nada
                detalle.Opcionales = null;
                //detalle.Iva --> agregamos uno
                AlicIva iva = new AlicIva { Id = 5, Importe = 21, BaseImp = 100 };
                detalle.Iva = new List<AlicIva>();
                detalle.Iva.Add(iva); 
                #endregion

                #region Completando detalle 2...
                detalle2.Concepto = 1;
                detalle2.DocTipo = 80;
                detalle2.DocNro = 20377033251;
                detalle2.FechaComp = new DateTime(2015, 7, 14);
                detalle2.ImporteTotal = 121;
                detalle2.ImporteTotalConc = 0;
                detalle2.ImporteNeto = 100;
                detalle2.ImporteIVA = 21;
                detalle2.ImporteOpExento = 0;
                detalle2.ImporteTrib = 0;
                detalle2.FechaServDesde = new DateTime();
                detalle2.FechaServHasta = new DateTime();
                detalle2.FechaVtoPago = new DateTime();
                detalle2.MonedaId = "PES";
                detalle2.MonedaCotiz = 1;
                //detalle.CbtesAsoc --> no agregamos nada
                detalle2.CbtesAsoc = null;
                //detalle.Tributos --> no agregamos nada
                detalle2.Tributos = null;
                //detalle.Opcionales --> no agregamos nada
                detalle2.Opcionales = null;
                //detalle.Iva --> agregamos uno
                AlicIva iva2 = new AlicIva { Id = 5, Importe = 21, BaseImp = 100 };
                detalle2.Iva = new List<AlicIva>();
                detalle2.Iva.Add(iva2);
                #endregion

                factura.DetalleFactura = new List<Detalle>();
                factura.DetalleFactura.Add(detalle);
                factura.DetalleFactura.Add(detalle2);
                #endregion

                Console.WriteLine("\n>\n>\n>\n>\n");

                FECAEResponse response = ws.ObtenerCAE(factura, cuit);

                #region Muestro resultados
                if ((response.FeCabResp != null) && (response.FeDetResp != null))
                {
                    switch (response.FeCabResp.Resultado)
                    {
                        case "A":
                            Console.WriteLine("Resultado general: " + response.FeCabResp.Resultado + " (Aprobado)\n");
                            foreach (FECAEDetResponse det in response.FeDetResp)
                            {
                                Console.WriteLine("- Nro. de comprobante: " + det.CbteDesde);
                                Console.WriteLine("- CUIT: " + det.DocNro);
                                Console.WriteLine("- CAE: " + det.CAE + ", con fecha de vto.: " + det.CAEFchVto);
                                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>");
                            }
                            break;
                        case "P":
                            Console.WriteLine("Resultado general: " + response.FeCabResp.Resultado + " (Parcial)\n");
                            foreach (FECAEDetResponse det in response.FeDetResp)
                            {
                                Console.WriteLine("- Resultado: " + det.Resultado);
                                Console.WriteLine("- Nro. de comprobante: " + det.CbteDesde);
                                Console.WriteLine("- CUIT: " + det.DocNro);
                                if (det.Resultado == "A")
                                {
                                    Console.WriteLine("- CAE: " + det.CAE + ", con fecha de vto.: " + det.CAEFchVto);
                                }
                                else
                                {
                                    foreach (Obs obs in det.Observaciones)
                                    {
                                        Console.WriteLine("- - Error: " + obs.Code + " -> " + obs.Msg);
                                        Console.WriteLine("--------------------");
                                    }
                                }
                                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>");
                            }
                            break;
                        case "R":
                            Console.WriteLine("Resultado general: " + response.FeCabResp.Resultado + " (Rechazado)\n");
                            foreach (Err err in response.Errors)
                            {
                                Console.WriteLine("Error: " + err.Code + " -> " + err.Msg);
                                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>");
                            }
                            break;
                    }
                }
                else
                {
                    foreach (Err err in response.Errors)
                    {
                        Console.WriteLine("Error: " + err.Code + " -> " + err.Msg);
                        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>");
                    }
                }
                //Console.Beep();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error. ");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("--------------------------------------------");
            }

            Console.ReadKey();

        }
    }
}
