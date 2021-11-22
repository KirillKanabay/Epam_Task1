using NewYearGift.BLL.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class SugarRangeValidationService : IValidationService<SugarRange>
    {
        public ValidationResponse Validate(SugarRange sugarRange)
        {
            var response = new ValidationResponse();
            
            if (sugarRange.MinWeight < 0)
            {
                response.AppendError("Минимальный вес сахара не может быть меньше 0\n");
            }

            if (sugarRange.MaxWeight < 0)
            {
                response.AppendError("Максимальный вес сахара не может быть меньше 0\n");
            }

            if (sugarRange.MinWeight > sugarRange.MaxWeight)
            {
                response.AppendError("Минимальный вес сахара не может быть больше максимального\n");
            }

            return response;
        }
    }
}