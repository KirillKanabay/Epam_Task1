using System;
using NewYearGift.BLL.Services;
using NewYearGift.Domain.Models;
using NewYearGift.Helpers;

namespace NewYearGift.Views
{
    public class SweetView : IItemView<Sweet>
    {
        private readonly ISweetService _sweetService;
        public SweetView(ISweetService sweetService)
        {
            _sweetService = sweetService;
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
                    ShowAll();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleExtensions.WriteLineError("Введенной команды не существует. Введите help для помощи.");
                    break;
            }
        }
        public Sweet SelectById(bool pause = true)
        {
            Clear();
            ShowAll(pause);

            Sweet sweet = null;

            while (true)
            {
                Console.Write($"{Environment.NewLine}" +
                              "Введите id конфеты (оставьте строку пустой для отмены ввода):");

                var consoleResponse = ConsoleExtensions.ReadInt();

                if (consoleResponse.IsFinishedInput)
                {
                    break;
                }

                if (consoleResponse.HasError)
                {
                    ConsoleExtensions.WriteLineError(consoleResponse.Error);
                }
                
                int sweetId = consoleResponse.Data;
                
                var sweetServiceResponse = _sweetService.GetById(sweetId);
                if (!sweetServiceResponse.IsSuccess)
                {
                    ConsoleExtensions.WriteLineError(sweetServiceResponse.Message);
                }
                else
                {
                    sweet = sweetServiceResponse.Data;
                    break;
                }
            }

            return sweet;
        }
        private void AddSweet()
        {
            Clear();
            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}" +
                                      $"Введите номер типа сладости (оставьте строку пустой для отмены ввода):{Environment.NewLine}" +
                                      $"Шоколадная конфета: 1 {Environment.NewLine}" +
                                      $"Леденец: 2 {Environment.NewLine}");

                    Console.Write("Тип сладости:");

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

                    int sweetType = intConsoleResponse.Data;
                    
                    Sweet sweet = sweetType switch
                    {
                        1 => new ChocolateSweet(),
                        2 => new Lollipop(),
                        _ => null
                    };

                    if (sweet == null)
                    {
                        ConsoleExtensions.WriteLineError("Неверный тип сладостей");
                        continue;
                    }
                    
                    Console.Write("Название:");
                    sweet.Name = Console.ReadLine();

                    Console.Write("Производитель:");
                    sweet.Manufacturer = Console.ReadLine();

                    Console.Write("Вес конфеты (грамм):");
                    var doubleConsoleResponse = ConsoleExtensions.ReadDouble();
                    if (doubleConsoleResponse.HasError)
                    {
                        ConsoleExtensions.WriteLineError(doubleConsoleResponse.Error);
                        continue;
                    }
                    sweet.Weight = doubleConsoleResponse.Data;

                    Console.Write("Количество сахара (грамм):");
                    doubleConsoleResponse = ConsoleExtensions.ReadDouble();
                    if (doubleConsoleResponse.HasError)
                    {
                        ConsoleExtensions.WriteLineError(doubleConsoleResponse.Error);
                        continue;
                    }
                    sweet.SugarWeight = doubleConsoleResponse.Data;

                    Console.Write("Стоимость (Br):");
                    var decimalConsoleResponse = ConsoleExtensions.ReadDecimal();
                    if (decimalConsoleResponse.HasError)
                    {
                        ConsoleExtensions.WriteLineError(decimalConsoleResponse.Error);
                        continue;
                    }
                    sweet.Price = decimalConsoleResponse.Data;

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

                    var response = _sweetService.Add(sweet);
                    if (!response.IsSuccess)
                    {
                        ConsoleExtensions.WriteLineError(response.Message);
                    }
                    if (!ConsoleExtensions.CheckContinue("Добавить еще одну запись? (y/n):")) break;
                    
                    Clear();
            }
           
        }
        private void DeleteSweet()
        {
            var sweet = SelectById();
            var response = _sweetService.Delete(sweet);
            if (!response.IsSuccess)
            {
                ConsoleExtensions.WriteLineError(response.Message);
            }
            Clear();
        }
        public void ShowAll(bool pause = true)
        {
            Clear();
            Console.WriteLine($"{Environment.NewLine}" +
                              "Список доступных сладостей:");
            var sweetsList = _sweetService.ListAll().Data;
            
            foreach (var sweet in sweetsList)
            {
                Console.WriteLine(sweet);    
            }

            if (pause)
            {
                Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
                Console.ReadKey();
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
