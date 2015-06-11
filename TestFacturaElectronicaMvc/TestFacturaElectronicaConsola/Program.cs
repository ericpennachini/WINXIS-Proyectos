using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFacturaElectronicaDominio;

namespace TestFacturaElectronicaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFacturaElectronicaDominio.FacturaElectronicaWS.ServiceSoapClient f = new TestFacturaElectronicaDominio.FacturaElectronicaWS.ServiceSoapClient();
            TestFacturaElectronicaDominio.FacturaElectronicaWS.FECAERequest request = new TestFacturaElectronicaDominio.FacturaElectronicaWS.FECAERequest();
            //request.FeCabReq.
            TestFacturaElectronicaDominio.FacturaElectronicaWS.FECAEResponse response = new TestFacturaElectronicaDominio.FacturaElectronicaWS.FECAEResponse();
            response = f.FECAESolicitar(
        }
    }
}
