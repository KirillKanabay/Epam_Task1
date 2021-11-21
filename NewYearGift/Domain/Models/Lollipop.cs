using NewYearGift.Models;

namespace NewYearGift.Domain.Models
{
    /// <summary>
    /// Класс леденца
    /// </summary>
    public class Lollipop:Sweet
    {
        /// <summary>
        /// Вкус 
        /// </summary>
        public string Flavor { get; set; }
        
        public Lollipop(int id, string name) : base(id, name) {}

        public override string ToString()
        {
            return
                $"Леденец: {Name}, Производитель: {Manufacturer}, Вес: {Weight} г., Кол-во сахара: {SugarWeight} г., Стоимость: {Price:C1}, Ароматизатор: {Flavor}";
        }
    }
}
