using NewYearGift.BLL.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public interface IValidationService<T>
    {
        ValidationResult Validate(T value);
    }
}