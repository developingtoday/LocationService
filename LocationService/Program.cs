﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var location=new LocationService();
            location.Start();
            Console.ReadKey();
        }
    }
}
