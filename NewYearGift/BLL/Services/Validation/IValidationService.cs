namespace NewYearGift.BLL.Services.Validation
{
    public interface IValidationService<T>
    {
        ValidationResponse Validate(T value);
    }
}