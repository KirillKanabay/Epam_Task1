using System;
using NewYearGift.Controllers;
using NewYearGift.Helpers;
using NewYearGift.Models;

namespace NewYearGift.Views
{
    public class GiftView
    {
        private readonly GiftController _giftController;
        private readonly SweetController _sweetController;
        public GiftView(GiftController giftController, SweetController sweetController)
        {
            _giftController = giftController;
            _sweetController = sweetController;
        }
        public void Show()
        {
            ShowHelp();
            while (true)
            {
                try
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
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);
                }
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
                    ShowGifts();
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
                    throw new ArgumentException("Введенной команды не существует. Введите help для помощи.");
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
            int giftId = SelectGift();
            if (giftId == -1)
            {
                return;
            }

            Console.WriteLine(_giftController.GetById(giftId));
        }
        private void OrderGift()
        {
            int giftId = SelectGift();
            if (giftId == -1)
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
                sortId = int.Parse(Console.ReadLine() ?? "-1");
                try
                {
                    _giftController.Order(giftId, (SweetsOrderRule)sortId);
                    break;
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);
                }
            } while (sortId != -1);
            Clear();
        }
        private int SelectGift()
        {
            Clear();
            ShowGifts();

            int giftId;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id подарка (оставьте строку пустой для отмены ввода):");
                giftId = int.Parse(Console.ReadLine() ?? "-1");
                try
                {
                    var gift = _giftController.GetById(giftId);
                    break;
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);
                }
            } while (giftId != -1);

            return giftId;
        }
        private void ShowGifts()
        {
            Clear();
            Console.WriteLine($"{Environment.NewLine}" +
                              $"Список подарков:");
            var giftsList = _giftController.GetAll();
            for (int giftIdx = 0; giftIdx < giftsList.Count; giftIdx++)
            {
                var gift = giftsList[giftIdx];
                Console.WriteLine($"Id:{giftIdx}, Название: {gift.Name}, суммарный вес:{gift.TotalWeight} г., суммарная стоимость: {gift.TotalPrice:C2}");
            }
        }
        private void DeleteGift()
        {
            Clear();
            ShowGifts();

            int id;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id подарка (оставьте строку пустой для отмены ввода):");
                id = int.Parse(Console.ReadLine() ?? "-1");
                try
                {
                    _giftController.Delete(id);
                    if (ConsoleExtensions.CheckContinue("Удалить еще одну запись? (y/n):")) continue;
                    Clear();
                    break;

                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);
                }
            } while (id != -1);

            Clear();
        }
        private void MakeGift()
        {
            int giftId = SelectGift();
            if (giftId == -1)
            {
                return;
            }

            ShowSweets();

            int sweetId;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id сладости (оставьте строку пустой для отмены ввода):");
                sweetId = int.Parse(Console.ReadLine() ?? "-1");
                try
                {
                    Console.Write("Введите количество сладостей:");
                    int count = int.Parse(Console.ReadLine() ?? "1");

                    _giftController.AddSweetToGift(giftId, sweetId, count);
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);
                }
            } while (sweetId != -1);
        }
        private void AddGift()
        {
            Clear();
            while (true)
            {
                try
                {
                    Console.Write("Введите название подарка:");
                    Gift gift = new Gift(Console.ReadLine());
                    _giftController.Add(gift);
                    Clear();

                    if (ConsoleExtensions.CheckContinue("Добавить еще одну запись? (y/n):")) continue;
                    Clear();
                    break;
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);

                    if (ConsoleExtensions.CheckContinue("Добавить еще одну запись? (y/n):")) continue;
                    Clear();
                    break;
                }
            }
        }
        private void ShowSweets()
        {
            Clear();
            Console.WriteLine($"{Environment.NewLine}" +
                              "Список доступных сладостей:");
            var sweetsList = _sweetController.GetAll();
            for (int sweetIdx = 0; sweetIdx < sweetsList.Count; sweetIdx++)
            {
                Console.WriteLine($"Id:{sweetIdx}, {sweetsList[sweetIdx]}");
            }
        }
        private void SugarRange()
        {
            int giftId = SelectGift();
            if (giftId == -1)
            {
                return;
            }

            Console.Write("Введите начальное содержание сахара:");
            int startValue = int.Parse(Console.ReadLine());

            Console.Write("Введите конечное содержание сахара:");
            int endValue = int.Parse(Console.ReadLine());

            try
            {
                var sweet = _giftController.FindSweetBySugarRange(giftId, startValue, endValue);
                Console.WriteLine(sweet == null ? "Такой конфеты не обнаружено" : sweet.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Управление подарками. Введите help - для справки ====");
        }
    }
}
