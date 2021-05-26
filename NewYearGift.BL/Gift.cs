using System;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.BL
{
    /// <summary>
    /// Класс подарка
    /// </summary>
    public class Gift
    {
        private string _name;
        private readonly List<SweetBase> _sweets;

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

                _name = Name;
            }
        }
        /// <summary>
        /// Количество конфет в подарке
        /// </summary>
        public int SweetsCount => _sweets.Count;
        /// <summary>
        /// Суммарный вес конфет
        /// </summary>
        public double TotalWeight => _sweets.Sum(s => s.Weight);
        /// <summary>
        /// Суммарная стоимость
        /// </summary>
        public decimal TotalPrice => _sweets.Sum(s => s.Price);
        
        public Gift(string name)
        {
            Name = name;
            _sweets = new List<SweetBase>();
        }

        /// <summary>
        /// Метод, добавляющий конфету в подарок
        /// </summary>
        /// <param name="sweet"></param>
        public void AddSweet(SweetBase sweet)
        {
            if (sweet == null)
            {
                throw new ArgumentNullException(nameof(sweet), "Сладость не может быть Null");
            }

            _sweets.Add(sweet);
        }

        /// <summary>
        /// Метод, удаляющий конфету из подарка
        /// </summary>
        /// <param name="sweetId"></param>
        public void RemoveSweet(int sweetId)
        {
            if (sweetId < 0 || sweetId >= _sweets.Count)
            {
                throw new ArgumentException("Не существует конфеты с таким номером");
            }

            _sweets.RemoveAt(sweetId);
        }


    }
}
