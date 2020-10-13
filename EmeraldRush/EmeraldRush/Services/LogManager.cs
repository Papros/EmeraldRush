using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Services
{
    class LogManager
    {
        public static void Print(string msg, int priority = 1)
        {
            Console.WriteLine(msg);
        }
    }
}
