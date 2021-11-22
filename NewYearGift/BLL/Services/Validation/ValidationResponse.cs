using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewYearGift.BLL.Services.Validation
{
    public class ValidationResponse
    {
        private readonly ICollection<string> _errors;
        public IEnumerable<string> Errors => _errors;
        public string Error { get; private set; }
        public bool HasError => _errors.Any();
        public bool IsSuccess => !HasError;
        
        public ValidationResponse()
        {
            _errors = new List<string>();
        }

        public void AppendError(string error)
        {
            Error += $"{error}\n";
            _errors.Add(error);
        }

        public void AppendErrorList(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                AppendError(error);
            }
        }
    }
}