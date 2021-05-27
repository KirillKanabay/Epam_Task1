using System;
using System.Collections.Generic;
using System.Text;
using NewYearGift.Models;

namespace NewYearGift.Services
{
    public interface IGiftService
    {
        /// <summary>
        /// Метод возвращающий все подарки
        /// </summary>
        /// <returns>Подарки</returns>
        List<Gift> GetAll();

        /// <summary>
        /// Получить подарок по идентификатору
        /// </summary>
        /// <param name="giftId">Идентификатор подарка</param>
        /// <returns>Подарок</returns>
        Gift GetById(int giftId);

        /// <summary>
        /// Метод добавляющий подарок
        /// </summary>
        /// <returns>Возвращает последнюю добавленный подарок</returns>
        Gift Add(Gift gift);

        /// <summary>
        /// Метод обновляющий подарок
        /// </summary>
        /// <param name="giftId">Идентификатор подарка</param>
        /// <param name="gift">Измененный подарок</param>
        /// <returns></returns>
        Gift Update(int giftId, Gift gift);

        /// <summary>
        /// Метод удаляющий подарок
        /// </summary>
        /// <param name="giftId">Идентификатор подарка</param>
        /// <returns>Удаленный подарок</returns>
        Gift Delete(int giftId);

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
