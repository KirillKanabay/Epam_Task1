using System;
using System.Collections.Generic;
using NewYearGift.Domain.Models;
using NewYearGift.Models;

namespace NewYearGift.Repositories
{
    public interface IGiftRepository
    {
        /// <summary>
        /// Метод добавляющий подарок
        /// </summary>
        /// <param name="item">Подарок</param>
        void Add(Gift item);
        
        /// <summary>
        /// Получение подарка по id
        /// </summary>
        /// <param name="id">Id подарка</param>
        /// <returns></returns>
        Gift GetById(int id); 
        
        /// <summary>
        /// Получение списка подарков
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Gift> ListAll();
        
        /// <summary>
        /// Получение списка подарков по определенному предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IReadOnlyList<Gift> List(Func<Gift, bool> predicate);
        
        /// <summary>
        /// Получение отсортированных подарков
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        IReadOnlyList<Gift> OrderBy(IComparer<Gift> comparer);
        
        /// <summary>
        /// Обновление подарка
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void Update(Gift item);
        
        /// <summary>
        /// Удаление подарка
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
