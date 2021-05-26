using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.BL
{
    /// <summary>
    /// Класс шоколадной конфеты
    /// </summary>
    public class ChocolateSweet:SweetBase
    {
        private string _kindOfChocolate;

        /// <summary>
        /// Тип шоколада
        /// </summary>
        public string KindOfChocolate
        {
            get => _kindOfChocolate;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Тип шоколада конфеты не может быть пустым.");
                }

                _kindOfChocolate = value;
            }
        }

        public ChocolateSweet(string name, string manufacturer, double weight, double sugarWeight, decimal price, string kindOfChocolate) 
            : base(name, manufacturer, weight, sugarWeight, price)
        {
            KindOfChocolate = kindOfChocolate;
        }
    }
}
