using System.Collections.Generic;
using System.Linq;
using NewYearGift.BLL.Comparers.Sweets;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.Domain.Models;
using NewYearGift.Models;

namespace NewYearGift.BLL.Services
{
    public class GiftEditorService : IGiftEditorService
    {
        private readonly IValidationService<SugarRange> _sugarRangeValidationService;
        private readonly IValidationService<GiftItem> _giftItemValidationService;
        public GiftEditorService(IValidationService<SugarRange> sugarRangeValidationService,
                                IValidationService<GiftItem> giftItemValidationService)
        
        {
            _sugarRangeValidationService = sugarRangeValidationService;
            _giftItemValidationService = giftItemValidationService;
        }
        
        public ServiceResponse<GiftItem> GetById(Gift gift, int id)
        {
            if (gift == null)
            {
                return new ServiceResponse<GiftItem>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            var foundGiftItem = gift.Items.FirstOrDefault(giftItem => giftItem.Id == id);

            if (foundGiftItem == null)
            {
                return new ServiceResponse<GiftItem>()
                {
                    IsSuccess = false,
                    Message = $"Элемент подарка с id: {id} не найден.",
                };
            }
            
            return new ServiceResponse<GiftItem>()
            {
                IsSuccess = true,
                Message = $"Элемент подарка с id: {id} найден.",
                Data = foundGiftItem,
            };
        }

        public ServiceResponse<IEnumerable<GiftItem>> ListAll(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<IEnumerable<GiftItem>>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }
            
            return new ServiceResponse<IEnumerable<GiftItem>>()
            {
                IsSuccess = true,
                Message = "Список элементов подарка",
                Data = gift.Items,
            };
        }

        public ServiceResponse<IEnumerable<Sweet>> GetSweetsBySugarRange(Gift gift, SugarRange sugarRange)
        {
            if (gift == null)
            {
                return new ServiceResponse<IEnumerable<Sweet>>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                    Data = null
                };
            }

            var validationResponse = _sugarRangeValidationService.Validate(sugarRange);
            if (validationResponse.HasError)
            {
                return new ServiceResponse<IEnumerable<Sweet>>()
                {
                    Message = validationResponse.Error,
                    IsSuccess = false,
                };
            }

            var sweets = gift.Items.Where(giftItem => giftItem.Sweet.Weight >= sugarRange.MinWeight
                                                      && giftItem.Sweet.Weight <= sugarRange.MaxWeight)
                .Select(giftItem => giftItem.Sweet);

            return new ServiceResponse<IEnumerable<Sweet>>()
            {
                IsSuccess = true,
                Data = sweets
            };
        }

        public ServiceResponse<IEnumerable<GiftItem>> OrderSweetsInGift(Gift gift, SweetsOrderRule sweetsOrderRule)
        {
            if (gift == null)
            {
                return new ServiceResponse<IEnumerable<GiftItem>>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            IComparer<Sweet> comparer = sweetsOrderRule switch
            {
                SweetsOrderRule.Name        => new SweetNameComparer(),
                SweetsOrderRule.Price       => new SweetPriceComparer(),
                SweetsOrderRule.Weight      => new SweetWeightComparer(),
                SweetsOrderRule.SugarWeight => new SweetSugarComparer(),
                _                           => null,
            };

            if (comparer == null)
            {
                return new ServiceResponse<IEnumerable<GiftItem>>()
                {
                    IsSuccess = false,
                    Message = "Не поддерживаемый тип сортировки",
                };
            }

            var orderedGiftItems = gift.Items.OrderBy(giftItem => giftItem.Sweet, comparer);

            return new ServiceResponse<IEnumerable<GiftItem>>()
            {
                IsSuccess = true,
                Data = orderedGiftItems
            };
        }

        public ServiceResponse<Gift> Add(Gift gift, GiftItem item)
        {
            if (gift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            if (item == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Элемент подарка не может быть NULL",
                };
            }

            var giftItemValidationResponse = _giftItemValidationService.Validate(item);

            if (giftItemValidationResponse.HasError)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = giftItemValidationResponse.Error,
                };
            }

            int newId = GetNewId(gift);
            item.Id = newId;
            gift.Items.Add(item);

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = gift,
            };
        }

        public ServiceResponse<Gift> Update(Gift gift, GiftItem item)
        {
            if (gift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            if (item == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Элемент подарка не может быть NULL",
                };
            }
            
            var giftItemValidationResponse = _giftItemValidationService.Validate(item);

            if (giftItemValidationResponse.HasError)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = giftItemValidationResponse.Error,
                };
            }
            
            var foundGiftItem = gift.Items.FirstOrDefault(giftItem => giftItem.Id == item.Id);

            if (foundGiftItem == null)
            {
                int newId = GetNewId(gift);
                item.Id = newId;
                gift.Items.Add(item);
            }
            
            int foundGiftItemIndex = gift.Items.IndexOf(foundGiftItem);
            gift.Items[foundGiftItemIndex] = item;

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = gift,
            };
        }

        public ServiceResponse<Gift> Delete(Gift gift, GiftItem item)
        {
            if (gift == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }
            
            if (item == null)
            {
                return new ServiceResponse<Gift>()
                {
                    IsSuccess = false,
                    Message = "Элемент подарка не может быть NULL",
                };
            }
            
            gift.Items.Remove(item);

            return new ServiceResponse<Gift>()
            {
                IsSuccess = true,
                Data = gift
            };
        }

        public ServiceResponse<int> SweetsCount(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<int>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            int sweetsCount = gift.Items.Sum(giftItem => giftItem.Count);

            return new ServiceResponse<int>()
            {
                IsSuccess = true,
                Data = sweetsCount,
            };
        }

        public ServiceResponse<double> TotalWeight(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<double>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            double totalWeight = gift.Items.Sum(giftItem => giftItem.Count * giftItem.Sweet.Weight);

            return new ServiceResponse<double>()
            {
                IsSuccess = true,
                Data = totalWeight,
            };
        }

        public ServiceResponse<decimal> TotalPrice(Gift gift)
        {
            if (gift == null)
            {
                return new ServiceResponse<decimal>()
                {
                    IsSuccess = false,
                    Message = "Подарок не может быть NULL",
                };
            }

            decimal totalPrice = gift.Items.Sum(giftItem => giftItem.Count * giftItem.Sweet.Price);

            return new ServiceResponse<decimal>()
            {
                IsSuccess = true,
                Data = totalPrice,
            };
        }

        private int GetNewId(Gift gift)
        {
            int lastId = gift.Items.Max(giftItem => giftItem.Id);
            return ++lastId;
        }
    }
}