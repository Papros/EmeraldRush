using System;

namespace EmeraldRush.Droid.Services
{

    class LogManager
    {
        public static void Print(string msg, string sender = "AppLog", int priority = 1)
        {
            Console.WriteLine("[" + sender + "] : " + msg);
        }

        public static void FastPrint(string msg, bool printFastMsg = true)
        {
            Console.WriteLine(" [ FAST_PRINT ] : " + msg);
        }
    }
}