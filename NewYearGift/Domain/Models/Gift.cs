using System.Collections.Generic;
using System.Text;

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
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
