using System.Collections.Generic;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Gifts
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IValidationService<Gift> _giftValidationService;
        public GiftService(IGiftRepository giftService, IValidationService<Gift> giftValidationService)
        {
            _giftRepository = giftService;
            _giftValidationService = giftValidationService;
        }

        public ServiceResponse<Gift> Add(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            var giftValidationResult = _giftValidationService.Validate(gift);

            if (giftValidationResult.HasError)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = giftValidationResult.Error,
                };
            }
            
            _giftRepository.Add(gift);

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = gift,
            };
        }

        public ServiceResponse<Gift> GetById(int giftId)
        {
            if (giftId < 0)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Id подарка не может быть меньше 0",
                };
            }

            var foundGift = _giftRepository.GetById(giftId);

            if (foundGift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = $"Подарок с id {giftId} не найден",
                };
            }

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = foundGift,
            };
        }

        public ServiceResponse<IEnumerable<Gift>> ListAll()
        {
            var gifts = _giftRepository.ListAll();
            return new ServiceResponse<IEnumerable<Gift>>()
            {
                IsSuccess = true,
                Data = gifts,
            };
        }

        public ServiceResponse<Gift> Update(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }
            
            var giftValidationResult = _giftValidationService.Validate(gift);

            if (giftValidationResult.HasError)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = giftValidationResult.Error,
                };
            }
            
            _giftRepository.Update(gift);

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = gift,
            };
        }

        public ServiceResponse<Gift> Delete(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }
            
            _giftRepository.Delete(gift);

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = gift,
            };
        }
    }
}
