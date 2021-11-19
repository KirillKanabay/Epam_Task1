﻿using System;

namespace NewYearGift.Models
{
    /// <summary>
    /// Базовый класс конфеты
    /// </summary>
    public abstract class Sweet 
    {
        /// <summary>
        /// Идентификатор конфеты
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название конфеты
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Производитель конфеты
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Вес конфеты
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Вес сахара в одной конфете
        /// </summary>
        public double SugarWeight { get; set; }

        /// <summary>
        /// Стоимость одной конфеты
        /// </summary>
        public decimal Price { get; set; }
        
        protected Sweet(int id, string name) {}
        
        protected Sweet(int id, string name, string manufacturer, double weight, double sugarWeight, decimal price)
        {
            Manufacturer = manufacturer;
            Weight = weight;
            SugarWeight = sugarWeight;
            Price = price;
        }
    }
}
