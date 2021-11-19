using System;

namespace NewYearGift.Models
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
        public Lollipop(int id,string name, string manufacturer, double weight, double sugarWeight, decimal price, string flavor) : base(id, name, manufacturer, weight, sugarWeight, price)
        {
            Flavor = flavor;
        }

        public Lollipop(int id, string name) : base(id, name) {}

        public override string ToString()
        {
            return
                $"Леденец: {Name}, Производитель: {Manufacturer}, Вес: {Weight} г., Кол-во сахара: {SugarWeight} г., Стоимость: {Price:C1}, Ароматизатор: {Flavor}";
        }
    }
}
