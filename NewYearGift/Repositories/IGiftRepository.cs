using System;
using System.Collections.Generic;
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
        IEnumerable<Gift> ListAll();
        
        /// <summary>
        /// Получение списка подарков по определенному предикату
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Gift> List(Func<Gift, bool> predicate);
        
        /// <summary>
        /// Получение отсортированных подарков
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        IEnumerable<Gift> OrderBy(IComparer<Gift> comparer);
        
        /// <summary>
        /// Обновление подарка
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void Update(int id, Gift item);
        
        /// <summary>
        /// Удаление подарка
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        
        /// <summary>
        /// Метод добавляющий сладость в подарок
        /// </summary>
        /// <param name="giftId">Идентификатор подарка</param>
        /// <param name="sweet"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        void AddSweetToGift(int giftId, Sweet sweet, int count);

        /// <summary>
        /// Метод производящий поиск конфеты по заданному диапазону содержания сахара в подарке
        /// </summary>
        /// <param name="startValue">Начальное значение количества сахара</param>
        /// <param name="endValue">Конечное значение количества сахара</param>
        /// <returns>Сладость подходящие под условие</returns>
        Sweet FindSweetBySugarRange(int giftId, int startValue, int endValue);

        /// <summary>
        /// Метод сортирующий конфеты в подарке по определенному правилу 
        /// </summary>
        /// <param name="giftId">Идентификатор подарка</param>
        /// <param name="orderRule">Правило сортировки</param>
        void Order(int giftId, SweetsOrderRule orderRule);
    }
}
