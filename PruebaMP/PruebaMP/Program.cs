using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mercadopago;
using System.Collections;

namespace PruebaMP
{
    class Program
    {
        static void Main(string[] args)
        {
            MP mp = new MP("1381942109699869", "wldQTAJ6PZxdshBAKc0Mc5cGI8D2X7ZD"); //([CLIENT_ID],[CLIENT_SECRET])
            string accessToken = "access_token=" + mp.getAccessToken();
            Console.WriteLine(accessToken);

            //crear usuario de prueba
            Hashtable request = new Hashtable();
            request.Add("site_id", "MLA");
            string uri = "/users/test_user?" + accessToken;
            Hashtable responseCreateUser = mp.post(uri, request);
            /*
            
                User 1:
                {
	                ["id"] = 188157777,
	                ["nickname"] = "TETE1736057"
	                ["password"] = "qatest3685",
	                ["email"] = "test_user_19813525@testuser.com",
	                ["site_status"] = "active"
                }

                User 2:
                {
	                ["id"] = 188154275,
	                ["nickname"] = "TETE9219635",
	                ["password"] = "qatest2029",
	                ["email"] = "test_user_18097226@testuser.com",
	                ["site_status"] = "active"
                }
          
            */

            Console.ReadKey();
        }
    }
}
