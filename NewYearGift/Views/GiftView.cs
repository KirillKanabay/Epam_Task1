using System;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services;
using NewYearGift.Domain.Models;
using NewYearGift.Helpers;
using NewYearGift.Models;

namespace NewYearGift.Views
{
    public class GiftView
    {
        private readonly IGiftService _giftService;
        private readonly ISweetService _sweetService;
        private readonly IGiftEditorService _giftEditorService;
        public GiftView(IGiftService giftService, ISweetService sweetService, IGiftEditorService giftEditorService)
        {
            _giftService = giftService;
            _sweetService = sweetService;
            _giftEditorService = giftEditorService;
        }
        public void Show()
        {
            ShowHelp();
            while (true)
            {
                Console.WriteLine();
                Console.Write(">>");
                string command = Console.ReadLine();

                if (command == "back")
                {
                    Program.Clear();
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
                case "add-gift":
                    AddGift();
                    break;
                case "make-gift":
                    MakeGift();
                    break;
                case "delete-gift":
                    DeleteGift();
                    break;
                case "show-gifts":
                    ShowAllGifts();
                    break;
                case "show-gift":
                    ShowGift();
                    break;
                case "order-gift":
                    OrderGift();
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
                              $"Добавить подарок: add-gift{Environment.NewLine}" +
                              $"Собрать подарок: make-gift{Environment.NewLine}" +
                              $"Вывести все подарки: show-gifts{Environment.NewLine}" +
                              $"Вывести все конфеты подарка: show-gift{Environment.NewLine}" +
                              $"Отсортировать конфеты в подарке: order-gift{Environment.NewLine}"+
                              $"Поиск конфеты по заданному диапазону содержания сахара в подарке: sugar-range{Environment.NewLine}"+
                              $"Удалить подарок: delete-gift{Environment.NewLine}" +
                              $"Вернуться в главное меню: back{Environment.NewLine}" +
                              $"Выйти из программы: exit{Environment.NewLine}"
            );
        }
        private void ShowGift()
        {
            var gift = SelectGiftById();
            if (gift == null)
            {
                return;
            }

            Console.WriteLine(gift);
        }
        private void OrderGift()
        {
            Gift gift = SelectGiftById();
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
                              "Введите номер сортировки (оставьте строку пустой для отмены ввода или введите -1):");
                sortId = int.Parse(Console.ReadLine() ?? "-1");
                _giftEditorService.OrderSweetsInGift(gift, (SweetsOrderRule)sortId);
            } while (sortId != -1);
            Clear();
        }
        private Gift SelectGiftById()
        {
            Clear();
            ShowAllGifts();

            Gift gift = null;

            int giftId;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id подарка (оставьте строку пустой для отмены ввода или введите -1):");
                
                giftId = int.Parse(Console.ReadLine() ?? "-1");

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
            } while (giftId != -1);

            return gift;
        }
        private void ShowAllGifts()
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
        }
        private void DeleteGift()
        {
            while (true)
            {
                var gift = SelectGiftById();
                if (gift == null)
                {
                    return;
                }
                _giftService.Delete(gift);
            
                if (ConsoleExtensions.CheckContinue("Удалить еще одну запись? (y/n):")) break;    
            }
        }
        private void MakeGift()
        {
            var gift = SelectGiftById();
            if (gift == null)
            {
                return;
            }

            ShowSweets();

            int sweetId;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id сладости (оставьте строку пустой для отмены ввода или введите -1):");
                sweetId = int.Parse(Console.ReadLine() ?? "-1");

                var sweetServiceResponse = _sweetService.GetById(sweetId);
                if (!sweetServiceResponse.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(sweetServiceResponse.Message);
                    continue;
                }
                
                Console.Write("Введите количество сладостей:");
                int count = int.Parse(Console.ReadLine() ?? "1");
                
                _giftEditorService.Add(gift, new GiftItem()
                {
                    Sweet = sweetServiceResponse.Data,
                    Count = count
                });
                
            } while (sweetId != -1);
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
        private void ShowSweets()
        {
            Clear();
            Console.WriteLine($"{Environment.NewLine}" +
                              "Список доступных сладостей:");
            var sweetsList = _sweetService.ListAll().Data;
            
            foreach (var sweet in sweetsList)
            {
                Console.WriteLine(sweet);    
            }
        }
        private void SugarRange()
        {
            var gift = SelectGiftById();
            if (gift == null)
            {
                return;
            }

            Console.Write("Введите начальное содержание сахара:");
            int minWeight = int.Parse(Console.ReadLine());

            Console.Write("Введите конечное содержание сахара:");
            int maxWeight = int.Parse(Console.ReadLine());

            var sugarRange = new SugarRange(minWeight, maxWeight);
            
            var giftEditorResponse = _giftEditorService.GetSweetsBySugarRange(gift, sugarRange);
            if (!giftEditorResponse.IsSuccess)
            {
                ConsoleExtensions.WriteLineError(giftEditorResponse.Message);
                return;
            }

            foreach (var sweet in giftEditorResponse.Data)
            {
                Console.WriteLine(sweet);
            }
        }
        private void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Управление подарками. Введите help - для справки ====");
        }
    }
}
