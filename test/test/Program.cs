﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = "SoloLearn";

            int x = s.Length;

            int y = s.IndexOf("e");

            Console.Write(x % y);
            Console.ReadKey();
        }
    }
}
