using System;
using NewYearGift.Domain.Models;
using NewYearGift.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class GiftItemValidator : IValidationService<GiftItem>
    {
        private readonly IValidationService<Sweet> _sweetValidationService;
        public GiftItemValidator(IValidationService<Sweet> sweetValidationService)
        {
            _sweetValidationService = sweetValidationService;
        }
        
        public ValidationResponse Validate(GiftItem giftItem)
        {
            var response = new ValidationResponse();
            
            if (giftItem.Sweet == null)
            {
                response.AppendError("Конфета не может быть null");
            }

            var sweetValidationResponse = _sweetValidationService.Validate(giftItem.Sweet);

            if (sweetValidationResponse.HasError)
            {
                response.AppendErrorList(sweetValidationResponse.Errors);
            }
            
            if (giftItem.Count <= 0)
            {
                response.AppendError("Количество конфет не может быть меньше или равно 0");
            }

            return response;
        }
    }
}