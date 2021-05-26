using System;
using System.Collections.Generic;
using NewYearGift.Models;

namespace NewYearGift
{
    class Program
    {
        private static readonly List<Sweet> SweetsList = new List<Sweet>()
        {
            new ChocolateSweet("Ромашка", "Коммунарка", weight: 10.4d, sugarWeight: 4.2d, price: 0.50m, "Молочный"),
            new ChocolateSweet("Черемуха", "Коммунарка", weight: 12.4d, sugarWeight: 3.5d, price: 0.45m, "Темный"),
            new Lollipop("Леденец", "Коммунарка", weight: 8d, sugarWeight: 6d, price: 0.22m, "Дюшес"),
            new Lollipop("Леденец", "Коммунарка", weight: 8d, sugarWeight: 6d, price: 0.22m, "Мята"),
            new Lollipop("Леденец", "Коммунарка", weight: 8d, sugarWeight: 6d, price: 0.22m, "Барбарис"),
        };
        private static Gift _gift;
        static void Main(string[] args)
        {
            Console.Title = "Сборщик новогодних подарков";
            GiftInit();
            Console.Title = $"Сборщик новогодних подарков: {_gift.Name}";

            Console.Clear();
            PrintHelp();

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write(">>");
                    DoCommand(Console.ReadLine());
                }
                catch (Exception e)
                {
                    WriteError(e.Message);
                }
            }
        }

        /// <summary>
        /// Инициализация подарка
        /// </summary>
        private static void GiftInit()
        {
            while (_gift == null)
            {
                try
                {
                    Console.Write("Введите название подарка: ");
                    string giftName = Console.ReadLine();
                    _gift = new Gift(giftName);
                }
                catch (Exception e)
                {
                    WriteError(e.Message);
                }
            }
        }
        /// <summary>
        /// Метод выводящий ошибку
        /// </summary>
        /// <param name="error">Ошибка</param>
        private static void WriteError(string error)
        {
            var defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(error);

            Console.ForegroundColor = defaultConsoleForeground;
        }
        /// <summary>
        /// Метод выводящий справку
        /// </summary>
        private static void PrintHelp()
        {
            Console.WriteLine($"Доступные команды: {Environment.NewLine}" +
                              $"Справка: help {Environment.NewLine}" +
                              $"Просмотреть подарок: view-gift {Environment.NewLine}" +
                              $"Отсортировать конфеты в подарке: order-gift {Environment.NewLine}" +
                              $"Просмотреть список доступных конфет: view-sweets {Environment.NewLine}" +
                              $"Добавить конфеты: add-sweets {Environment.NewLine}" +
                              $"Найти конфету в подарке, соответствующую заданному диапазону содержания сахара: find-sweet-sugar{Environment.NewLine}" +
                              $"Закончить работы: exit {Environment.NewLine}"
            );
        }
        /// <summary>
        /// Метод выполняющий введенные команды
        /// </summary>
        /// <param name="command"></param>
        private static void DoCommand(string command)
        {
            switch (command.ToLower())
            {
                case "help":
                    Console.Clear();
                    PrintHelp();
                    break;
                case "view-gift":
                    Console.Clear();
                    ViewGift();
                    break;
                case "view-sweets":
                    Console.Clear();
                    PrintSweetsList();
                    break;
                case "add-sweets":
                    Console.Clear();
                    AddSweetsToGift();
                    break;
                case "find-sweet-sugar":
                    Console.Clear();
                    FindSweetBySugar();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    throw new ArgumentException("Введенной команды не существует. Введите help для помощи.");
            }
        }
        /// <summary>
        /// Метод выводящий информацию о подарке
        /// </summary>
        private static void ViewGift()
        {
            Console.WriteLine(_gift);
        }
        /// <summary>
        /// Метод выводящий список конфет 
        /// </summary>
        private static void PrintSweetsList()
        {
            Console.WriteLine("Список доступных сладостей:");

            for (int sweetIdx = 0; sweetIdx < SweetsList.Count; sweetIdx++)
            {
                Console.WriteLine($"Id:{sweetIdx}, {SweetsList[sweetIdx]}");
            }
        }
        /// <summary>
        /// Метод добавляющий конфеты в подарок
        /// </summary>
        private static void AddSweetsToGift()
        {
            while (true)
            {
                PrintSweetsList();
                Console.WriteLine();
                Console.Write("Введите id добавляемой конфеты (-1 для выхода):");

                int sweetId = int.Parse(Console.ReadLine());
                if (sweetId == -1)
                {
                    break;
                }
                if (sweetId < 0 && sweetId >= SweetsList.Count)
                {
                    WriteError("Такой конфеты не существует");
                    continue;
                }

                Console.Write("Введите количество добавляемых конфет:");
                int count = int.Parse(Console.ReadLine());
                if (count <= 0)
                {
                    WriteError("Количество добавляемых конфет не может быть меньше или равно нулю.");
                    continue;
                }

                _gift.AddSweet(SweetsList[sweetId], count);
            }
        }

        private static void OrderGift()
        {
            Console.WriteLine($"Отсортировать список конфет по:{Environment.NewLine}" +
                              $"1. Имени{Environment.NewLine}" +
                              $"2. Производителю{Environment.NewLine}" +
                              $"3. Весу{Environment.NewLine}" +
                              $"4. Количеству сахара{Environment.NewLine}" +
                              $"5. Стоимости{Environment.NewLine}" +
                              $"6. Количеству.{Environment.NewLine}");
        }

        /// <summary>
        /// Метод находящий конфету в подарке по диапазону содержания сахара
        /// </summary>
        private static void FindSweetBySugar()
        {

        }
    }
}
