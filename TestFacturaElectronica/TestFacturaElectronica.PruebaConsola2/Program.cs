﻿using System;
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
            Console.Write("Ingrese CUIT: ");
            long cuit = Convert.ToInt64(Console.ReadLine());

            Service1Client ws = new Service1Client();


        }
    }
}
