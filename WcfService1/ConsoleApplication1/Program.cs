using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1.ServicioPrueba;
using WcfService1;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client service = new Service1Client();


            Console.Write("Ingresar: ");
            Console.WriteLine(service.GetData(Convert.ToInt32(Console.ReadLine())));
            Console.ReadKey();

            Console.Write("Ingresar valor bool: ");
            CompositeType compo = new CompositeType();
            compo.BoolValue = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Ingresar valor string: ");
            compo.StringValue = Console.ReadLine();
            Console.WriteLine(service.GetDataUsingDataContract(compo));

            Console.WriteLine("Composite type:");
            Console.WriteLine("Bool value: {0} - String value: {1}", compo.BoolValue, compo.StringValue);

            Console.ReadKey();

        }
    }
}
