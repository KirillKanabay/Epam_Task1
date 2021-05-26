using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewYearGift.Models
{
    /// <summary>
    /// Класс подарка
    /// </summary>
    public class Gift
    {
        private string _name;
        
        /// <summary>
        /// Словарь хранит конфету и их количество
        /// </summary>
        private Dictionary<Sweet, int> _sweets;

        /// <summary>
        /// Название подарка
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название подарка не может быть пустым");
                }

                _name = value;
            }
        }
        /// <summary>
        /// Количество конфет в подарке
        /// </summary>
        public int SweetsCount => _sweets.Count;
        /// <summary>
        /// Суммарный вес конфет
        /// </summary>
        public double TotalWeight => _sweets.Sum(s => s.Key.Weight * s.Value);
        /// <summary>
        /// Суммарная стоимость
        /// </summary>
        public decimal TotalPrice => _sweets.Sum(s => s.Key.Price * s.Value);
        
        public Gift(string name)
        {
            Name = name;
            _sweets = new Dictionary<Sweet, int>();
        }

        /// <summary>
        /// Метод добавляющий конфеты в подарок
        /// </summary>
        /// <param name="sweet">Конфета</param>
        /// <param name="count">Количество</param>
        public void AddSweet(Sweet sweet, int count = 1)
        {
            if (sweet == null)
            {
                throw new ArgumentNullException(nameof(sweet), "Сладость не может быть Null");
            }

            if (count <= 0)
            {
                throw new ArgumentException("Количество конфет не может быть меньше или равно нулю");
            }
            _sweets.Add(sweet, count);
        }

        /// <summary>
        /// Сортировка списка конфет по параметру
        /// </summary>
        /// <param name="sweetsOrderRule"></param>
        public void OrderBy(SweetsOrderRule sweetsOrderRule)
        {
            _sweets = sweetsOrderRule switch
            {
                SweetsOrderRule.Name => new Dictionary<Sweet, int>(_sweets.OrderBy(s => s.Key.Name)),
                SweetsOrderRule.Manufacturer => new Dictionary<Sweet, int>(_sweets.OrderBy(s => s.Key.Manufacturer)),
                SweetsOrderRule.Price => new Dictionary<Sweet, int>(_sweets.OrderBy(s => s.Key.Price)),
                SweetsOrderRule.Weight => new Dictionary<Sweet, int>(_sweets.OrderBy(s => s.Key.Weight)),
                SweetsOrderRule.SugarWeight => new Dictionary<Sweet, int>(_sweets.OrderBy(s => s.Key.SugarWeight)),
                SweetsOrderRule.Count => new Dictionary<Sweet, int>(_sweets.OrderBy(s => s.Value)),
                _ => throw new ArgumentOutOfRangeException(nameof(sweetsOrderRule), sweetsOrderRule, null)
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Стоимость подарка: {TotalPrice:C2}");
            sb.AppendLine($"Вес подарка: {TotalWeight} г.");
            sb.AppendLine($"Список конфет:");
            
            int index = 1;
            foreach (var sweet in _sweets)
            {
                sb.AppendLine($"{index}. {sweet.Key}, Количество: {sweet.Value}");
                index++;
            }

            return sb.ToString();
        }
    }
}
