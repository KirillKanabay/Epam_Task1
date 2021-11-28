using System;
using NewYearGift.Domain.Models;
using NewYearGift.Views.Helpers;

namespace NewYearGift.Views
{
    public class MainView : IView
    {
        private readonly IItemView<Gift> _giftView;
        private readonly IItemView<Sweet> _sweetView;
        
        public MainView(IItemView<Gift> giftView, IItemView<Sweet> sweetView)
        {
            _giftView = giftView;
            _sweetView = sweetView;
        }
        public void Show()
        {
            Console.Title = "Сборщик новогодних подарков";
            
            while (true)
            {
                Clear();
                ShowHelp();
                
                Console.WriteLine();
                Console.Write(">>");
                DoCommand(Console.ReadLine());
            }
        }
        public void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Сборщик новогодних подарков. Введите help - для справки ====");
        }

        /// <summary>
        /// Метод выводящий справку
        /// </summary>
        private void ShowHelp()
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
        private void DoCommand(string command)
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
                    ConsoleExtensions.WriteLineError("Введенной команды не существует. Введите help для помощи.");
                    break;
            }
        }
    }
}