using System.Collections.Generic;
using NewYearGift.Domain.Models;

namespace NewYearGift.DAL.Repositories
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
        /// Обновление подарка
        /// </summary>
        /// <param name="item"></param>
        void Update(Gift item);
        
        /// <summary>
        /// Удаление подарка
        /// </summary>
        /// <param name="gift"></param>
        void Delete(Gift gift);
    }
}
