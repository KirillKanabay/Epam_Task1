using System;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class GiftValidator : IValidationService<Gift>
    {
        public ValidationResponse Validate(Gift item)
        {
            var response = new ValidationResponse();

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                response.AppendError("Имя подарка не может быть пустым");
            }

            return response;
        }
    }
}