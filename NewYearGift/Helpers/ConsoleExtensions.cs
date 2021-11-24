using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.Helpers
{
    public static class ConsoleExtensions
    {
        public static void WriteLineError(string error)
        {
            WriteLineWithColor(error, ConsoleColor.Red);
        }
        public static bool CheckContinue(string message)
        {
            Console.Write(message);
            char key = Console.ReadKey().KeyChar;
            return char.ToLower(key) == 'y';
        }
        public static void WriteLineWithColor(string message, ConsoleColor foregroundColor)
        {
            var defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;

            Console.WriteLine(message);

            Console.ForegroundColor = defaultConsoleForeground;
        }
        
        public static ConsoleResponse<double> ReadDouble()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ConsoleResponse<double>()
                {
                    IsFinishedInput = true,
                };
            }

            double value = 0;
            
            if (!double.TryParse(input, out value))
            {
                return new ConsoleResponse<double>()
                {
                    Error = "Неправильный формат ввода"
                };
            }

            return new ConsoleResponse<double>()
            {
                Data = value,
            };
        }
        public static ConsoleResponse<int> ReadInt()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ConsoleResponse<int>()
                {
                    IsFinishedInput = true,
                };
            }

            int value = 0;
            
            if (!int.TryParse(input, out value))
            {
                return new ConsoleResponse<int>()
                {
                    Error = "Неправильный формат ввода"
                };
            }

            return new ConsoleResponse<int>()
            {
                Data = value,
            };
        }
        public static ConsoleResponse<decimal> ReadDecimal()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ConsoleResponse<decimal>()
                {
                    IsFinishedInput = true,
                };
            }

            decimal value = 0;
            
            if (!decimal.TryParse(input, out value))
            {
                return new ConsoleResponse<decimal>()
                {
                    Error = "Неправильный формат ввода"
                };
            }

            return new ConsoleResponse<decimal>()
            {
                Data = value,
            };
        }
    }
}
