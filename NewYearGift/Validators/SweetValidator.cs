using System;
using NewYearGift.Models;

namespace NewYearGift.Validators
{
    public class SweetValidator : IValidator<Sweet>
    {
        public void Validate(Sweet item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Sweet can't be null");
            }

            if (item.Id < 0)
            {
                throw new ArgumentException("Sweet's id can't be less than 0", nameof(item.Id));
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Sweet's name can't be empty", nameof(item.Name));
            }
            
            if (string.IsNullOrWhiteSpace(item.Manufacturer))
            {
                throw new ArgumentException("Sweet's manufacturer can't be empty", nameof(item.Manufacturer));
            }

            if (item.Price < 0)
            {
                throw new ArgumentException("Sweet's price can't be less than 0", nameof(item.Price));
            }

            if (item.Weight < 0)
            {
                throw new ArgumentException("Sweet's weight can't be less than 0", nameof(item.Weight));
            }

            if (item.SugarWeight < 0)
            {
                throw new ArgumentException("Sweet's sugar weight can't be less than 0", nameof(item.SugarWeight));
            }

            if (item.SugarWeight > item.Weight)
            {
                throw new ArgumentException("Sweet's sugar weight can't be greater than sweet's weight",
                    nameof(item.SugarWeight));
            }
        }
    }
}