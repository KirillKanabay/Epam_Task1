using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewYearGift.BLL.Comparers.Sweets;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.Models;

namespace NewYearGift.Domain.Models
{
    /// <summary>
    /// Класс подарка
    /// </summary>
    public class Gift
    {
        /// <summary>
        /// Идентификатор подарка
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Название подарка
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Содержит список позиций подарка
        /// </summary>
        public IList<GiftItem> Items { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Стоимость подарка: {TotalPrice:C2}");
            sb.AppendLine($"Вес подарка: {TotalWeight} г.");
            sb.AppendLine($"Список конфет:");
            
            foreach (var giftItem in Items)
            {
                sb.AppendLine($"{giftItem.Key}. {giftItem.Value.Sweet}, Количество: {giftItem.Value.Count}");
            }

            return sb.ToString();
        }
    }
}
