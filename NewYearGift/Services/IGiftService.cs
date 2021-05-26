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
        /// <param name="id">Идентификатор подарка</param>
        /// <returns>Подарок</returns>
        Gift GetById(int id);

        /// <summary>
        /// Метод добавляющий подарок
        /// </summary>
        /// <returns>Возвращает последнюю добавленный подарок</returns>
        Gift Add();

        /// <summary>
        /// Метод обновляющий подарок
        /// </summary>
        /// <param name="id">Идентификатор подарка</param>
        /// <param name="gift">Измененный подарок</param>
        /// <returns></returns>
        Gift Update(int id, Gift gift);

        /// <summary>
        /// Метод удаляющий подарок
        /// </summary>
        /// <param name="id">Идентификатор подарка</param>
        /// <returns>Удаленный подарок</returns>
        Gift Delete(int id);
    }
}
