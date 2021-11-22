using System;
using System.Collections.Generic;
using NewYearGift.Domain.Models;

namespace NewYearGift.DAL.Repositories
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
        IReadOnlyList<Sweet> ListAll();

        /// <summary>
        /// Обновление конфеты, если ее не существует, добавляется новая
        /// </summary>
        /// <param name="item"></param>
        void Update(Sweet item);
        
        /// <summary>
        /// Удаление конфеты
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
