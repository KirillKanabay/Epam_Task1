using System;

namespace NewYearGift.Models
{
    /// <summary>
    /// Класс шоколадной конфеты
    /// </summary>
    public class ChocolateSweet : Sweet
    {
        /// <summary>
        /// Тип шоколада
        /// </summary>
        public string KindOfChocolate { get; set; }

        public ChocolateSweet() { }

        public ChocolateSweet(string name, string manufacturer, double weight, double sugarWeight, decimal price,
            string kindOfChocolate)
            : base(name, manufacturer, weight, sugarWeight, price)
        {
            KindOfChocolate = kindOfChocolate;
        }

        public override string ToString()
        {
            return
                $"Шоколадная конфета: {Name}, Производитель: {Manufacturer}, Вес: {Weight} г., Кол-во сахара: {SugarWeight} г., Стоимость: {Price:C1}, Шоколад: {KindOfChocolate}";
        }
    }
}