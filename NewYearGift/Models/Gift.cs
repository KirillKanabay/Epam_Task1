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
        /// <summary>
        /// Словарь хранит конфету и их количество
        /// </summary>
        public Dictionary<Sweet, int> Sweets = new Dictionary<Sweet, int>();
        /// <summary>
        /// Название подарка
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Количество конфет в подарке
        /// </summary>
        public int SweetsCount => Sweets.Sum(s => s.Value);
        /// <summary>
        /// Суммарный вес конфет
        /// </summary>
        public double TotalWeight => Sweets.Sum(s => s.Key.Weight * s.Value);
        /// <summary>
        /// Суммарная стоимость
        /// </summary>
        public decimal TotalPrice => Sweets.Sum(s => s.Key.Price * s.Value);
        
        public Gift(string name)
        {
            Name = name;
        }
        
        /// <summary>
        /// Сортировка списка конфет по параметру
        /// </summary>
        /// <param name="sweetsOrderRule"></param>
        public void OrderBy(SweetsOrderRule sweetsOrderRule)
        {
            Sweets = sweetsOrderRule switch
            {
                SweetsOrderRule.Name => new Dictionary<Sweet, int>(Sweets.OrderBy(s => s.Key.Name)),
                SweetsOrderRule.Manufacturer => new Dictionary<Sweet, int>(Sweets.OrderBy(s => s.Key.Manufacturer)),
                SweetsOrderRule.Price => new Dictionary<Sweet, int>(Sweets.OrderBy(s => s.Key.Price)),
                SweetsOrderRule.Weight => new Dictionary<Sweet, int>(Sweets.OrderBy(s => s.Key.Weight)),
                SweetsOrderRule.SugarWeight => new Dictionary<Sweet, int>(Sweets.OrderBy(s => s.Key.SugarWeight)),
                SweetsOrderRule.Count => new Dictionary<Sweet, int>(Sweets.OrderBy(s => s.Value)),
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
            foreach (var sweet in Sweets)
            {
                sb.AppendLine($"{index}. {sweet.Key}, Количество: {sweet.Value}");
                index++;
            }

            return sb.ToString();
        }
    }
}
