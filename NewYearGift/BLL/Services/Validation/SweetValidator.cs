using System;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class SweetValidator : IValidationService<Sweet>
    {
        public ValidationResponse Validate(Sweet sweet)
        {
            var response = new ValidationResponse();
            
            if (sweet.Id < 0)
            {
                response.AppendError("Id конфеты не может быть меньше нуля");
            }

            if (string.IsNullOrWhiteSpace(sweet.Name))
            {
                response.AppendError("Название конфеты не может быть пустым");
            }
            
            if (string.IsNullOrWhiteSpace(sweet.Manufacturer))
            {
                response.AppendError("У конфеты должен быть производитель");
            }

            if (sweet.Price < 0)
            {
                response.AppendError("У конфеты не может быть стоимость меньше 0");
            }

            if (sweet.Weight < 0)
            {
                response.AppendError("У конфеты не может быть вес меньше 0");
            }

            if (sweet.SugarWeight < 0)
            {
                response.AppendError("У конфеты не может быть вес сахара меньше 0");
            }

            if (sweet.SugarWeight > sweet.Weight)
            {
                response.AppendError("Вес конфеты не может быть меньше веса сахара");
            }

            return response;
        }
    }
}