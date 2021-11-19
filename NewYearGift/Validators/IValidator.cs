namespace NewYearGift.Validators
{
    public interface IValidator<in T> where T : class
    {
        void Validate(T item);
    }
}