﻿using System;
using System.Collections.Generic;
using NewYearGift.BLL.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public interface IValidationResultBuilder<T>
    {
        IValidationResultBuilder<T> AppendRule(Predicate<T> rule, string message);
        IValidationResultBuilder<T> AppendErrors(IEnumerable<string> errors);
        ValidationResult Build();
    }
}