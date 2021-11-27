using System;
using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Validation
{
    public class SweetValidationService : IValidationService<Sweet>
    {
        public ValidationResult Validate(Sweet sweet)
        {
            var builder = new ValidationResultBuilder<Sweet>(sweet);

            builder.AppendRule(s => s.Id >= 0, "Id конфеты не может быть меньше нуля")
                .AppendRule(s => !string.IsNullOrWhiteSpace(s.Name), "Название конфеты не может быть пустым")
                .AppendRule(s => !string.IsNullOrWhiteSpace(s.Manufacturer), "У конфеты должен быть производитель")
                .AppendRule(s => s.Price >= 0, "У конфеты не может быть стоимость меньше 0")
                .AppendRule(s => s.Weight >= 0, "У конфеты не может быть вес меньше 0")
                .AppendRule(s => s.SugarWeight >= 0, "У конфеты не может быть вес сахара меньше 0")
                .AppendRule(s => s.SugarWeight <= s.Weight, "Вес конфеты не может быть меньше веса сахара");
            
            return builder.Build();
        }
    }
}