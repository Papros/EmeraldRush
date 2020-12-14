using System;

namespace EmeraldRush.Droid.Services
{

    class LogManager
    {
        public static void Print(string msg, string senderName = "AppLog", int priority = 1)
        {
            Console.WriteLine("[" + senderName + "] : " + msg);
        }

        public static void FastPrint(string msg, bool printFastMsg = true)
        {
            Console.WriteLine(" [ FAST_PRINT ] : " + msg);
        }
    }
}