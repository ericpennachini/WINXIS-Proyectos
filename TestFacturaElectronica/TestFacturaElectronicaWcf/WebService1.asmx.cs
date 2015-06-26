using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TestFacturaElectronicaDominio;


namespace TestFacturaElectronicaWcf
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private ServFactElect _servicioFacturacion;
        private Autorizacion _servicioAutorizacion;

        public ServFactElect ServicioFacturacion
        {
            get { return _servicioFacturacion; }
            set { _servicioFacturacion = value; }
        }
        public Autorizacion ServicioAutorizacion
        {
            get { return _servicioAutorizacion; }
            set { _servicioAutorizacion = value; }
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public 


        
    }
}
