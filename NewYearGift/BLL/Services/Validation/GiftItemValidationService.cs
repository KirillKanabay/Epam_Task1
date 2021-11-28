using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class GiftItemValidationService : IValidationService<GiftItem>
    {
        private readonly IValidationService<Sweet> _sweetValidationService;
        public GiftItemValidationService(IValidationService<Sweet> sweetValidationService)
        {
            _sweetValidationService = sweetValidationService;
        }
        
        public ValidationResult Validate(GiftItem giftItem)
        {
            var builder = new ValidationResultBuilder<GiftItem>(giftItem);

            builder.AppendRule(gi => gi.Sweet != null, "Конфета не может быть пустой")
                .AppendRule(gi => gi.Count > 0, "Количество конфет не может быть меньше или равно 0");
                   
            return builder.Build();
        }
    }
}