using System;
using System.Collections.Generic;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;
using NewYearGift.Helpers;
using NewYearGift.Models;
using NewYearGift.Views;

namespace NewYearGift
{
    class Program
    {
        private static readonly IDictionary<int, Sweet> SweetsList = new Dictionary<int, Sweet>()
        {
            {
                1,
                new ChocolateSweet()
                {
                    Id = 1,
                    Name = "Ромашка",
                    Manufacturer = "Коммунарка",
                    Weight = 10.4d,
                    SugarWeight = 4.2d,
                    Price = 0.5m,
                    KindOfChocolate = "Молочный",
                } 
            },
            {
              2,
              new ChocolateSweet()
              {
                  Id = 2,
                  Name = "Черемуха",
                  Manufacturer = "Коммунарка",
                  Weight = 12.4d,
                  SugarWeight = 3.5d,
                  Price = 0.45m,
                  KindOfChocolate = "Темный",
              } 
            },
            {
                3,
                new Lollipop()
                {
                    Id = 3,
                    Name = "Леденец",
                    Manufacturer = "Коммунарка",
                    Weight = 8d,
                    SugarWeight = 6d,
                    Price = 0.22m,
                    Flavor = "Дюшес",
                } 
            },
            {
                4,
                new Lollipop()
                {
                    Id = 4,
                    Name = "Леденец",
                    Manufacturer = "Коммунарка",
                    Weight = 8d,
                    SugarWeight = 6d,
                    Price = 0.22m,
                    Flavor = "Мята",
                } 
            },
            {
                5,
                new Lollipop()
                {
                    Id = 5,
                    Name = "Леденец",
                    Manufacturer = "Коммунарка",
                    Weight = 8d,
                    SugarWeight = 6d,
                    Price = 0.22m,
                    Flavor = "Барбарис",
                } 
            },
        };
        
        private static ISweetRepository _sweetRepository;
        private static IGiftRepository _giftRepository;
        
        private static IGiftService _giftService;
        private static IGiftEditorService _giftItemsService;
        
        private static IValidationService<Gift> _giftValidationService;
        private static IValidationService<GiftItem> _giftItemValidationService;
        private static IValidationService<SugarRange> _sugarRangeValidationService;
        private static IValidationService<Sweet> _sweetValidationService;
        
        private static GiftView _giftView;
        private static SweetView _sweetView;
        

        // private static ISweetService _sweetController;
        private static void InitDependencies()
        {
            _giftRepository = new GiftInMemoryRepository();
            _sweetRepository = new SweetInMemoryRepository();

            _sweetValidationService = new SweetValidationService();
            _giftValidationService = new GiftValidationService();
            _giftItemValidationService = new GiftItemValidationService(_sweetValidationService);
            _sugarRangeValidationService = new SugarRangeValidationService();
            
            _giftService = new GiftService(_giftRepository, _giftValidationService);
            _giftItemsService = new GiftEditorService(_sugarRangeValidationService, _giftItemValidationService);
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
