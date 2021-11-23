using System;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services;
using NewYearGift.Domain.Models;
using NewYearGift.Helpers;
using NewYearGift.Models;

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
                    ShowAll();
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
            var gift = SelectById();
            if (gift == null)
            {
                return;
            }

            Console.WriteLine(gift);
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
        private void OrderGift()
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
                              "Введите номер сортировки (оставьте строку пустой для отмены ввода или введите -1):");
                sortId = int.Parse(Console.ReadLine() ?? "-1");
                _giftEditorService.OrderSweetsInGift(gift, (SweetsOrderRule)sortId);
            } while (sortId != -1);
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }
        public Gift SelectById()
        {
            Clear();
            ShowAll();

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
        public void ShowAll()
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
            
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
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
        private void MakeGift()
        {
            var gift = SelectById();
            if (gift == null)
            {
                return;
            }
            
            Sweet sweet = null;
            do
            {
                sweet = _sweetView.SelectById();
                Console.Write("Введите количество сладостей:");
                int count = int.Parse(Console.ReadLine() ?? "1");
                
                var response = _giftEditorService.Add(gift, new GiftItem()
                {
                    Sweet = sweet,
                    Count = count
                });

                if (!response.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(response.Message);
                    break;
                }
            } while (sweet != null);
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
