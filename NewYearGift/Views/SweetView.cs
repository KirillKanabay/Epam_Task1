using System;
using NewYearGift.BLL.Services;
using NewYearGift.Domain.Models;
using NewYearGift.Helpers;

namespace NewYearGift.Views
{
    public class SweetView
    {
        private readonly SweetService _sweetService;

        public SweetView(SweetService sweetService)
        {
            _sweetService = sweetService;
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
                    Clear();
                    ShowHelp();
                    break;
                case "add-sweet":
                    Clear();
                    AddSweet();
                    break;
                case "delete-sweet":
                    Clear();
                    DeleteSweet();
                    break;
                case "show-sweets":
                    Clear();
                    ShowSweets();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleExtensions.WriteLineError("Введенной команды не существует. Введите help для помощи.");
                    break;
            }
        }

        private void AddSweet()
        {
            Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine($"{Environment.NewLine}" +
                                      $"Введите номер типа сладости:{Environment.NewLine}" +
                                      $"Шоколадная конфета: 1 {Environment.NewLine}" +
                                      $"Леденец: 2 {Environment.NewLine}");

                    Console.Write("Тип сладости:");
                    int type = int.Parse(Console.ReadLine() ?? "");
                    
                    Sweet sweet = type switch
                    {
                        1 => new ChocolateSweet(),
                        2 => new Lollipop(),
                        _ => throw new ArgumentException("Неверный тип сладости.")
                    };
                    
                    Console.Write("Название:");
                    sweet.Name = Console.ReadLine();

                    Console.Write("Производитель:");
                    sweet.Manufacturer = Console.ReadLine();

                    Console.Write("Вес конфеты (грамм):");
                    sweet.Weight = double.Parse(Console.ReadLine());

                    Console.Write("Количество сахара (грамм):");
                    sweet.SugarWeight = double.Parse(Console.ReadLine());

                    Console.Write("Стоимость (Br):");
                    sweet.Price = decimal.Parse(Console.ReadLine());

                    if (sweet is ChocolateSweet)
                    {
                        Console.Write("Тип шоколада:");
                        ((ChocolateSweet) sweet).KindOfChocolate = Console.ReadLine();
                    }
                    else if(sweet is Lollipop)
                    {
                        Console.Write("Ароматизатор:");
                        ((Lollipop) sweet).Flavor = Console.ReadLine();
                    }

                    _sweetController.Add(sweet);
                    
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

        private void DeleteSweet()
        {
            Clear();
            ShowSweets();

            int id;
            do
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id сладости (оставьте строку пустой для отмены ввода):");
                id = int.Parse(Console.ReadLine() ?? "-1");
                try
                {
                    _sweetController.Delete(id);
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteLineError(e.Message);
                }
            } while (id != -1);

            Clear();
        }

        public void ShowSweets()
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

        private void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Управление сладостями. Введите help - для справки ====");
        }

        private void ShowHelp()
        {
            Clear();
            Console.WriteLine($"Доступные команды:{Environment.NewLine}" +
                              $"Добавить сладость: add-sweet{Environment.NewLine}"+
                              $"Удалить сладость: delete-sweet{Environment.NewLine}"+
                              $"Вывести все сладости: show-sweets{Environment.NewLine}"+
                              $"Вернуться в главное меню: back{Environment.NewLine}"+
                              $"Выйти из программы: exit{Environment.NewLine}"
                              );
        }
    }
}
