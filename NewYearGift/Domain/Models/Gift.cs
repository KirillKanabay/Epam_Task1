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
        public int Id { get; set; }

        /// <summary>
        /// Название подарка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержит список позиций подарка
        /// </summary>
        public IList<GiftItem> Items { get; }

        public Gift()
        {
            Items = new List<GiftItem>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Items)
            {
                sb.Append(item);
            }

            return sb.ToString();
        }
    }
}
