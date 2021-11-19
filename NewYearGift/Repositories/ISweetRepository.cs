using System;
using System.Collections.Generic;
using NewYearGift.Models;

namespace NewYearGift.Repositories
{
    public interface ISweetRepository
    {
        /// <summary>
        /// Метод добавляющий конфету
        /// </summary>
        /// <param name="item">Конфета</param>
        void Add(Sweet item);
        
        /// <summary>
        /// Получение конфеты по id
        /// </summary>
        /// <param name="id">Id конфеты</param>
        /// <returns></returns>
        Sweet GetById(int id); 
        
        /// <summary>
        /// Получение списка конфет
        /// </summary>
        /// <returns></returns>
        IEnumerable<Sweet> ListAll();
        
        /// <summary>
        /// Получение списка конфет по определенному предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Sweet> List(Func<Sweet, bool> predicate);
        
        /// <summary>
        /// Получение отсортированных конфет
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        IEnumerable<Sweet> OrderBy(IComparer<Sweet> comparer);
        
        /// <summary>
        /// Обновление конфеты
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void Update(int id, Sweet item);
        
        /// <summary>
        /// Удаление конфеты
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
