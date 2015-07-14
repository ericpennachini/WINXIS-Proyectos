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

                #region Completando la factura (para probar)...
                factura.CantRegistros = 1;
                factura.PuntoVenta = 1;
                factura.TipoComprobante = 1;
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

                factura.DetalleFactura = new List<Detalle>();
                factura.DetalleFactura.Add(detalle);
                #endregion

                FECAEResponse response = ws.ObtenerCAE(factura, cuit);

                #region Muestro resultados
                switch (response.FeCabResp.Resultado)
                {
                    case "A":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Aprobado)");
                        Console.WriteLine("CAE: " + response.FeDetResp[0].CAE + ", con fecha de vto.: " + response.FeDetResp[0].CAEFchVto);
                        Console.ReadKey();
                        break;
                    case "P":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Parcial)");
                        if (response.FeDetResp != null)
                        {
                            foreach (Evt evt in response.Events)
                            {
                                Console.WriteLine("Observación: " + evt.Code + " -> " + evt.Msg);
                                Console.WriteLine("-------------");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case "R":
                        Console.WriteLine("Resultado: " + response.FeCabResp.Resultado + " (Rechazado)");
                        if (response.FeDetResp != null)
                        {
                            foreach (FECAEDetResponse r in response.FeDetResp)
                            {
                                foreach (Obs o in r.Observaciones)
                                {
                                    Console.WriteLine("Error: " + o.Code + " -> " + o.Msg);
                                    Console.WriteLine("-------------");
                                }
                            }
                        }
                        Console.ReadKey();
                        break;
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
                Console.ReadKey();
            }
        }
    }
}
