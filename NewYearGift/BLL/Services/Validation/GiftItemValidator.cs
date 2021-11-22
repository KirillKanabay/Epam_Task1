using System;
using NewYearGift.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class GiftItemValidator : IValidationService<GiftItem>
    {
        public void Validate(GiftItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "GiftItem can't be null");
            }

            if (item.Sweet == null)
            {
                throw new ArgumentException("Sweet can't be null", nameof(item.Count));
            }
            
            if (item.Count <= 0)
            {
                throw new ArgumentException("Sweets count can't be less than 0", nameof(item.Count));
            }
        }
    }
}