using System;

namespace NewYearGift.Models
{
    /// <summary>
    /// Базовый класс сладости
    /// </summary>
    public abstract class Sweet{
        /// <summary>
        /// Название сладости
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Производитель сладости
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Вес сладости
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Вес сахара в одной сладости
        /// </summary>
        public double SugarWeight { get; set; }

        /// <summary>
        /// Стоимость одной сладости
        /// </summary>
        public decimal Price { get; set; }

        protected Sweet(string name, string manufacturer, double weight, double sugarWeight, decimal price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Weight = weight;
            SugarWeight = sugarWeight;
            Price = price;
        }

        protected Sweet(){}
    }
}
