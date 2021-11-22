using NewYearGift.Domain.Models;

namespace NewYearGift.Models
{
    /// <summary>
    /// Позиция в подарке
    /// </summary>
    public abstract class GiftItem
    {
        public int Id { get; }
        /// <summary>
        /// Конфета
        /// </summary>
        public Sweet Sweet { get; }
        
        /// <summary>
        /// Количество конфет
        /// </summary>
        public int Count { get; }

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