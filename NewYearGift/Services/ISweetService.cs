using System.Collections.Generic;
using NewYearGift.Models;

namespace NewYearGift.Services
{
    public interface ISweetService
    {
        /// <summary>
        /// Метод возвращающий все сладости
        /// </summary>
        /// <returns>Подарки</returns>
        List<Sweet> GetAll();

        /// <summary>
        /// Получить сладость по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сладости</param>
        /// <returns>Сладость</returns>
        Sweet GetById(int id);

        /// <summary>
        /// Метод добавляющий сладость
        /// </summary>
        /// <returns>Возвращает последнюю добавленную сладость</returns>
        Sweet Add(Sweet sweet);

        /// <summary>
        /// Метод обновляющий сладость
        /// </summary>
        /// <param name="id">Идентификатор сладости</param>
        /// <param name="sweet">Измененная сладость</param>
        /// <returns>Возвращает измененную сладость</returns>
        Sweet Update(int id, Sweet sweet);

        /// <summary>
        /// Метод удаляющий сладость
        /// </summary>
        /// <param name="id">Идентификатор сладости</param>
        /// <returns>Удаленная сладость</returns>
        Sweet Delete(int id);
    }
}
