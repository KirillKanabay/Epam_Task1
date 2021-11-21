using NewYearGift.Models;

namespace NewYearGift.Domain.Models
{
    /// <summary>
    /// Класс шоколадной конфеты
    /// </summary>
    public class ChocolateSweet : Sweet
    {
        /// <summary>
        /// Тип шоколада
        /// </summary>
        public string KindOfChocolate { get; set; }

        public ChocolateSweet(int id, string name) : base(id, name) { }
        
        public override string ToString()
        {
            return
                $"Шоколадная конфета: {Name}, Производитель: {Manufacturer}, Вес: {Weight} г., Кол-во сахара: {SugarWeight} г., Стоимость: {Price:C1}, Шоколад: {KindOfChocolate}";
        }
    }
}