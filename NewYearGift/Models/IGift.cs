using System;
using System.Collections.Generic;

namespace NewYearGift.Models
{
    public interface IGift
    {
        /// <summary>
        /// Название подарка
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Количество конфет в подарке
        /// </summary>
        public int SweetsCount { get; }
        
        /// <summary>
        /// Суммарный вес конфет
        /// </summary>
        public double TotalWeight { get; }
        
        /// <summary>
        /// Суммарная стоимость
        /// </summary>
        public decimal TotalPrice { get; }
        
        /// <summary>
        /// Метод добавляющий позицию подарка
        /// </summary>
        /// <param name="item">Позиция подарка</param>
        void Add(GiftItem item);
        
        /// <summary>
        /// Получение позиции подарка по id
        /// </summary>
        /// <param name="id">Id подарка</param>
        /// <returns></returns>
        GiftItem GetById(int id); 
        
        /// <summary>
        /// Получение списка позиций подарка
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<GiftItem> ListAll();
        
        /// <summary>
        /// Получение списка позиций подарка по определенному предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IReadOnlyList<GiftItem> List(Func<GiftItem, bool> predicate);
        
        /// <summary>
        /// Получение отсортированных позиций подарка
        /// </summary>
        /// <param name="sweetsOrderRule"></param>
        /// <returns></returns>
        IReadOnlyList<GiftItem> OrderBy(SweetsOrderRule sweetsOrderRule);

        IReadOnlyList<GiftItem> OrderBy(IComparer<GiftItem> comparer);
        /// <summary>
        /// Обновление позиции подарка
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void Update(GiftItem item);
        
        /// <summary>
        /// Удаление позиции подарка
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        
        /// <summary>
        /// Поиск конфет в подарке по заданному диапазону содержания сахара
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        IReadOnlyList<Sweet> GetSweetsBySugarRange(double min, double max);
    }
}