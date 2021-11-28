using System;
using NewYearGift.BLL.Enums;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services.Gifts;
using NewYearGift.Domain.Models;
using NewYearGift.Views.Helpers;

namespace NewYearGift.Views
{
    public class GiftView : IItemView<Gift>
    {
        private readonly IGiftService _giftService;
        private readonly IGiftEditorService _giftEditorService;
        private readonly IItemView<Sweet> _sweetView;
        public GiftView(IGiftService giftService, IGiftEditorService giftEditorService, IItemView<Sweet> sweetView)
        {
            _giftService = giftService;
            _giftEditorService = giftEditorService;
            _sweetView = sweetView;
        }
        public void Show()
        {
            ShowHelp();
            while (true)
            {
                Clear();
                ShowHelp();
                
                Console.WriteLine();
                Console.Write(">>");
                string command = Console.ReadLine();

                if (command == "back")
                {
                    break;
                }

                DoCommand(command);
            }
        }
        private void DoCommand(string command)
        {
            switch (command.ToLower())
            {
                case "help":
                    ShowHelp();
                    break;
                case "add":
                    AddGift();
                    break;
                case "build":
                    BuildGift();
                    break;
                case "delete":
                    DeleteGift();
                    break;
                case "list":
                    ShowAll(pause: true);
                    break;
                case "sweets":
                    ShowGiftSweets();
                    break;
                case "order":
                    OrderSweetsInGift();
                    break;
                case "sugar-range":
                    SugarRange();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleExtensions.WriteLineError("Введенной команды не существует. Введите help для помощи.");
                    break;
            }
        }
        private void ShowHelp()
        {
            Clear();
            Console.WriteLine($"Доступные команды:{Environment.NewLine}" +
                              $"Добавить подарок: add{Environment.NewLine}" +
                              $"Собрать подарок: build{Environment.NewLine}" +
                              $"Вывести все подарки: list{Environment.NewLine}" +
                              $"Вывести все конфеты подарка: sweets{Environment.NewLine}" +
                              $"Отсортировать конфеты в подарке: order{Environment.NewLine}"+
                              $"Поиск конфеты по заданному диапазону содержания сахара в подарке: sugar-range{Environment.NewLine}"+
                              $"Удалить подарок: delete{Environment.NewLine}" +
                              $"Вернуться в главное меню: back{Environment.NewLine}" +
                              $"Выйти из программы: exit{Environment.NewLine}"
            );
        }
        private void ShowGiftSweets()
        {
            var gift = SelectById();
            if (gift == null)
            {
                return;
            }

            Console.WriteLine(gift);
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
        private void OrderSweetsInGift()
        {
            Gift gift = SelectById();
            if (gift == null)
            {
                return;
            }

            Console.WriteLine($"Отсортировать список конфет в подарке по:{Environment.NewLine}" +
                              $"Имени: 1{Environment.NewLine}" +
                              $"Производителю: 2{Environment.NewLine}" +
                              $"Весу: 3{Environment.NewLine}" +
                              $"Количеству сахара: 4{Environment.NewLine}" +
                              $"Стоимости: 5{Environment.NewLine}" +
                              $"Количеству: 6{Environment.NewLine}");
            int sortId;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите номер сортировки (оставьте строку пустой для отмены ввода):");

                string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                
                if (!int.TryParse(input, out sortId))
                {
                    ConsoleExtensions.WriteLineError("Неправильный формат ввода");    
                }
                
                var response = _giftEditorService.OrderSweetsInGift(gift, (SweetsOrderRule)sortId);

                if (!response.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(response.Message);
                }
                else
                {
                    foreach (var giftItem in response.Data)
                    {
                        Console.WriteLine(giftItem);
                    }
                }
                
            } while (sortId != -1);
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
        public Gift SelectById(bool pause = false)
        {
            Clear();
            ShowAll(pause);

            Gift gift = null;

            while(true)
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id подарка (оставьте строку пустой для отмены ввода):");

                var intConsoleResponse = ConsoleExtensions.ReadInt();
                
                if (intConsoleResponse.HasError)
                {
                    ConsoleExtensions.WriteLineError(intConsoleResponse.Error);
                    continue;
                }

                if (intConsoleResponse.IsFinishedInput)
                {
                    break;
                }

                int giftId = intConsoleResponse.Data;
                
                var response = _giftService.GetById(giftId);
                if (!response.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(response.Message);
                }
                else
                {
                    gift = response.Data;
                    break;
                }
            }

            return gift;
        }
        public void ShowAll(bool pause = false)
        {
            Clear();
            Console.WriteLine($"{Environment.NewLine}" +
                              $"Список подарков:");
            
            var giftsList = _giftService.ListAll().Data;
            
            foreach (var gift in giftsList)
            {
                double totalWeight = _giftEditorService.TotalWeight(gift).Data;
                decimal totalPrice = _giftEditorService.TotalPrice(gift).Data;
                
                Console.WriteLine($"Id:{gift.Id}, Название: {gift.Name}, суммарный вес:{totalWeight} г.," +
                                  $" суммарная стоимость: {totalPrice:C2}");    
            }

            if (pause)
            {
                Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
                Console.ReadKey();
            }
        }
        private void DeleteGift()
        {
            while (true)
            {
                var gift = SelectById();
                if (gift == null)
                {
                    return;
                }
                _giftService.Delete(gift);
            
                if (ConsoleExtensions.CheckContinue("Удалить еще одну запись? (y/n):")) break;    
            }
        }
        private void BuildGift()
        {
            var gift = SelectById();
            if (gift == null)
            {
                return;
            }
            
            while (true)
            {
                Sweet sweet = _sweetView.SelectById(false);
                
                Console.Write("Введите количество сладостей:");
                var intConsoleResponse = ConsoleExtensions.ReadInt();

                if (intConsoleResponse.IsFinishedInput)
                {
                    break;
                }
                
                if (intConsoleResponse.HasError)
                {
                    ConsoleExtensions.WriteLineError(intConsoleResponse.Error);
                    continue;
                }
                
                int sweetsCount = intConsoleResponse.Data;
                
                var response = _giftEditorService.Add(gift, new GiftItem()
                {
                    Sweet = sweet,
                    Count = sweetsCount
                });

                if (!response.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(response.Message);
                }
            }
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
        private void AddGift()
        {
            Clear();
            while (true)
            {
                Console.Write("Введите название подарка:");
                string giftName = Console.ReadLine();
                Gift gift = new Gift() {Name = giftName};
                var giftServiceResponse = _giftService.Add(gift);

                if (!giftServiceResponse.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(giftServiceResponse.Message);
                }
                
                if (!ConsoleExtensions.CheckContinue("Добавить еще одну запись? (y/n):")) break;
                Clear();
            }
        }
        private void SugarRange()
        {
            var gift = SelectById();
            if (gift == null)
            {
                return;
            }

            while (true)
            {
                Console.Write("Введите начальное содержание сахара:");
                var intConsoleResponse = ConsoleExtensions.ReadInt();
            
                if (intConsoleResponse.HasError)
                {
                    ConsoleExtensions.WriteLineError(intConsoleResponse.Error);
                    continue;
                }
            
                int minWeight = intConsoleResponse.Data;

                Console.Write("Введите конечное содержание сахара:");
                intConsoleResponse = ConsoleExtensions.ReadInt();
            
                if (intConsoleResponse.HasError)
                {
                    ConsoleExtensions.WriteLineError(intConsoleResponse.Error);
                    continue;
                }
            
                int maxWeight = intConsoleResponse.Data;
            
                var sugarRange = new SugarRange(minWeight, maxWeight);
            
                var giftEditorResponse = _giftEditorService.GetSweetsBySugarRange(gift, sugarRange);
                if (!giftEditorResponse.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(giftEditorResponse.Message);
                    continue;
                }

                foreach (var sweet in giftEditorResponse.Data)
                {
                    Console.WriteLine(sweet);
                }
                
                break;
            }
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
        private void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Управление подарками. Введите help - для справки ====");
        }
    }
}
