using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class GiftValidationService : IValidationService<Gift>
    {
        public ValidationResult Validate(Gift gift)
        {
            var builder = new ValidationResultBuilder<Gift>(gift);
            builder.AppendRule(g => !string.IsNullOrWhiteSpace(g.Name), "Имя подарка не может быть пустым");
            
            return builder.Build();
        }
    }
}