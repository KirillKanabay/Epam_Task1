using System;
using System.Collections.Generic;
using NewYearGift.BLL.Services;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;
using NewYearGift.Helpers;
using NewYearGift.Views;

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


        private static ISweetRepository _sweetRepository;
        private static IGiftRepository _giftRepository;
        private static GiftView _giftView;
        private static SweetView _sweetView;
        private static GiftService _giftController;
        private static SweetService _sweetController;
        private static void InitDependencies()
        {
            _giftRepository = new GiftInMemoryRepository();
            _sweetRepository = new SweetInMemoryRepository();

            _giftController = new GiftService(_giftRepository, _sweetRepository);
            _sweetController = new SweetService(_sweetRepository);

            _giftView = new GiftView(_giftController, _sweetController);
            _sweetView = new SweetView(_sweetController);
        }

        static void Main(string[] args)
        {
            InitDependencies();

            Console.Title = "Сборщик новогодних подарков";
            Clear();
            ShowHelp();
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
                    ConsoleExtensions.WriteLineError(e.Message);
                }
            }
        }
        public static void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Сборщик новогодних подарков. Введите help - для справки ====");
        }

        /// <summary>
        /// Метод выводящий справку
        /// </summary>
        private static void ShowHelp()
        {
            Clear();
            Console.WriteLine($"Доступные команды: {Environment.NewLine}" +
                              $"Управление подарками: gifts {Environment.NewLine}" +
                              $"Управление сладостями: sweets {Environment.NewLine}");
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
                    ShowHelp();
                    break;
                case "gifts":
                    _giftView.Show();
                    break;
                case "sweets":
                    _sweetView.Show();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    throw new ArgumentException("Введенной команды не существует. Введите help для помощи.");
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
    }
}
