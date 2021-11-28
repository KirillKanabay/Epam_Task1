using System.Collections.Generic;

namespace NewYearGift.BLL.Models
{
    public class ValidationResult
    {
        public IEnumerable<string> Errors { get; set; }
        public string Error { get; set; }
        public bool HasError { get; set; }
        public bool IsSuccess { get; set; }
    }
}