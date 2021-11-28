using System.Linq;

namespace NewYearGift.Views.Helpers
{
    public class ConsoleResponse<T>
    {
        public T Data { get; set; }
        public bool IsFinishedInput { get; set; }
        public string Error { get; set; }
        public bool HasError => Error?.Any() ?? false;

    }
}