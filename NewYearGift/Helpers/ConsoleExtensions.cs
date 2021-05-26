using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.Helpers
{
    public static class ConsoleExtensions
    {
        public static void WriteError(string error)
        {
            var defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(error);

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
