using System.Collections.Generic;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Sweets
{
    public class SweetService : ISweetService
    {
        private readonly ISweetRepository _sweetRepository;
        private readonly IValidationService<Sweet> _sweetValidationService;
        
        public SweetService(ISweetRepository sweetRepository, IValidationService<Sweet> sweetValidationService)
        {
            _sweetRepository = sweetRepository;
            _sweetValidationService = sweetValidationService;
        }
        
        public ServiceResponse<Sweet> Add(Sweet sweet)
        {
            if (sweet == null)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = "Конфета не может быть NULL",
                };
            }

            var sweetValidationResult = _sweetValidationService.Validate(sweet);

            if (sweetValidationResult.HasError)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = sweetValidationResult.Error,
                };
            }
            
            _sweetRepository.Add(sweet);

            return new ServiceResponse<Sweet>()
            {
                IsSuccess = true,
                Message = "Конфета успешно добавлена",
                Data = sweet,
            };
        }

        public ServiceResponse<Sweet> GetById(int sweetId)
        {
            if (sweetId < 0)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = "Id конфеты не может быть меньше 0",
                };
            }

            var foundSweet = _sweetRepository.GetById(sweetId);

            if (foundSweet == null)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = $"Конфета с id {sweetId} не найдена",
                };
            }

            return new ServiceResponse<Sweet>()
            {
                IsSuccess = true,
                Data = foundSweet,
            };
        }

        public ServiceResponse<IEnumerable<Sweet>> ListAll()
        {
            var sweets = _sweetRepository.ListAll();
            return new ServiceResponse<IEnumerable<Sweet>>()
            {
                IsSuccess = true,
                Data = sweets,
            };
        }

        public ServiceResponse<Sweet> Update(Sweet sweet)
        {
            if (sweet == null)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = "Конфета не может быть NULL",
                };
            }
            
            var sweetValidationResult = _sweetValidationService.Validate(sweet);

            if (sweetValidationResult.HasError)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = sweetValidationResult.Error,
                };
            }
            
            _sweetRepository.Update(sweet);

            return new ServiceResponse<Sweet>()
            {
                IsSuccess = true,
                Data = sweet,
            };
        }

        public ServiceResponse<Sweet> Delete(Sweet sweet)
        {
            if (sweet == null)
            {
                return new ServiceResponse<Sweet>()
                {
                    IsSuccess = false,
                    Message = "Конфета не может быть NULL",
                };
            }
            
            _sweetRepository.Delete(sweet);

            return new ServiceResponse<Sweet>()
            {
                IsSuccess = true,
                Data = sweet,
            };
        }
    }
}
