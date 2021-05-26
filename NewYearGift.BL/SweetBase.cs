using System;

namespace NewYearGift.BL
{
    /// <summary>
    /// Базовый класс сладости
    /// </summary>
    abstract class SweetBase
    {
        private string _name;
        private string _manufacturer;
        private double _weight;
        private double _sugarWeight;
        private decimal _price;
       
        /// <summary>
        /// Название сладости
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название сладости не может быть пустым.", nameof(Name));
                }

                _name = value;
            }
        }

        /// <summary>
        /// Производитель сладости
        /// </summary>
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название производителя не может быть пустым.");
                }

                _manufacturer = value;
            }
        }

        /// <summary>
        /// Вес сладости
        /// </summary>
        public double Weight
        {
            get => _weight;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Вес сладости не может быть меньше или равен нулю.", nameof(Weight));
                }

                _weight = value;
            }
        }

        /// <summary>
        /// Вес сахара в одной сладости
        /// </summary>
        public double SugarWeight
        {
            get => _sugarWeight;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Вес сахара в сладости не может быть меньше или равно нулю.");
                }

                _sugarWeight = value;
            }
        }

        /// <summary>
        /// Стоимость одной сладости
        /// </summary>
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Стоимость конфеты не может быть меньше нуля.");
                }

                _price = value;
            }
        }

        protected SweetBase(string name, string manufacturer, double weight, double sugarWeight, decimal price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Weight = weight;
            SugarWeight = sugarWeight;
            Price = price;
        }
    }
}
