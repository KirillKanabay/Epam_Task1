namespace NewYearGift.BLL.Models
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = true;
        public T Data { get; set; }
    }
}