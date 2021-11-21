namespace NewYearGift.BLL.Services.Validation
{
    public interface IValidator<in T> where T : class
    {
        void Validate(T item);
    }
}