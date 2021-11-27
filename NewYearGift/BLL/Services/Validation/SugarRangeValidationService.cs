using NewYearGift.BLL.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class SugarRangeValidationService : IValidationService<SugarRange>
    {
        public ValidationResult Validate(SugarRange sugarRange)
        {
            var builder = new ValidationResultBuilder<SugarRange>(sugarRange);

            builder.AppendRule(sr => sr.MinWeight >= 0, "Минимальный вес сахара не может быть меньше 0")
                .AppendRule(sr => sr.MaxWeight >= 0, "Максимальный вес сахара не может быть меньше 0")
                .AppendRule(sr => sr.MinWeight <= sr.MaxWeight, "Минимальный вес сахара не может быть больше максимального");
                

            return builder.Build();
        }
    }
}