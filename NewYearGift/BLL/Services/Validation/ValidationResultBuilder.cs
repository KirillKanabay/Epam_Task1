using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGift.BLL.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class ValidationResultBuilder<T> : IValidationResultBuilder<T>
    {
        private readonly ICollection<string> _errors;
        public string Error { get; private set; }
        public bool HasError => _errors.Any();
        public bool IsSuccess => !HasError;
        private readonly T _validationObject;

        public ValidationResultBuilder(T validationObject)
        {
            _errors = new List<string>();
            _validationObject = validationObject;
        }

        public IValidationResultBuilder<T> AppendRule(Predicate<T> rule, string errorMessage)
        {
            if (!rule?.Invoke(_validationObject) ?? false)
            {
                AppendError(errorMessage);
            }

            return this;
        }

        public ValidationResult Build()
        {
            return new ValidationResult()
            {
                Error = Error,
                Errors = _errors,
                IsSuccess = IsSuccess,
                HasError = HasError,
            };
        }

        private void AppendError(string error)
        {
            Error += $"{error}\n";
            _errors.Add(error);
        }
    }
}