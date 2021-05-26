using System;
using System.Collections.Generic;
using System.Text;
using NewYearGift.Controllers;
using NewYearGift.Helpers;
using NewYearGift.Models;

namespace NewYearGift.Views
{
    public class SweetView
    {
        private readonly SweetController _sweetController;

        public SweetView(SweetController sweetController)
        {
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
                    ConsoleExtensions.WriteError(e.Message);
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
                    throw new ArgumentException("Введенной команды не существует. Введите help для помощи.");
            }
        }

        private void AddSweet()
        {
            Clear();
            while (true)
            {
                try
                {
                    Console.WriteLine($"Введите номер типа сладости:{Environment.NewLine}" +
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
                        Console.Write("Вкус:");
                        ((Lollipop) sweet).Flavor = Console.ReadLine();
                    }

                    _sweetController.Add(sweet);
                    break;
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteError(e.Message);
                }
            }
        }

        private void DeleteSweet()
        {
            Clear();
            ShowSweets();
            Console.Write("Введите id сладости:");
            int id = int.Parse(Console.ReadLine());
            try
            {
                _sweetController.Delete(id);
            }
            catch (Exception e)
            {
                ConsoleExtensions.WriteError(e.Message);
            }
            
        }

        private void ShowSweets()
        {
            Clear();
            Console.WriteLine("Список доступных сладостей:");
            var sweetsList = _sweetController.GetAll();
            for (int sweetIdx = 0; sweetIdx < sweetsList.Count; sweetIdx++)
            {
                Console.WriteLine($"Id:{sweetIdx}, {sweetsList[sweetIdx]}");
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
