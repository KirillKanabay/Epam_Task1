using NewYearGift.Domain.Models;

namespace NewYearGift.Models
{
    /// <summary>
    /// Позиция в подарке
    /// </summary>
    public class GiftItem
    {
        public int Id { get; set; }
        /// <summary>
        /// Конфета
        /// </summary>
        public Sweet Sweet { get; set; }
        
        /// <summary>
        /// Количество конфет
        /// </summary>
        public int Count { get; set; }

        public GiftItem()
        {
            
        }
        public GiftItem(int id, Sweet sweet, int count)
        {
            Id = id;
            Sweet = sweet;
            Count = count;
        }
    }
}