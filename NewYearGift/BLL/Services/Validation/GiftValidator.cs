using System;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class GiftValidator : IValidator<Gift>
    {
        public void Validate(Gift item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item),"Gift can't be null");
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Gift's name can't be empty", nameof(item.Name));
            }
        }
    }
}