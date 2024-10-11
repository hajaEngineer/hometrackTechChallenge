using System;

namespace PurgomalumAPIAutomation.Utilities
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}