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
            //try
            //{
            
            loginCmsRequest logReq = new loginCmsRequest();
            logReq.in0 = "C:\\Program Files (x86)\\PyAfipWs\\pennachini_prueba_wsass.crt";
            
                
                ServiceSoapClient f = new ServiceSoapClient();
                ServFactElect servicio = new ServFactElect();
                FEAuthRequest autorizacion = new FEAuthRequest();
                FECAERequest request = new FECAERequest();
                FECAEResponse response = new FECAEResponse();
                autorizacion = servicio.Autorizar();
                request = servicio.ConfigurarRequest();

                response = f.FECAESolicitar(autorizacion, request); //necesita una autorizacion y el request
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Ocurrio el siguiente error: \n\n" + ex.Message);
            //    Console.WriteLine("\n \n- Tipo de excepcion: " + ex.GetType());
            //    Console.WriteLine("\n \n- En: " + ex.TargetSite);
            //    Console.ReadKey();
            //}
            
        }
    }
}
