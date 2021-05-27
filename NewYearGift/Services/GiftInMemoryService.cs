using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGift.Models;

namespace NewYearGift.Services
{
    public class GiftInMemoryService:IGiftService
    {
        private readonly List<Gift> _giftsList = new List<Gift>();
        public List<Gift> GetAll() => _giftsList;
        public Gift GetById(int giftId)
        {
            if (giftId < 0 || giftId >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }
            return _giftsList[giftId];
        }
        public Gift Add(Gift gift)
        {
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift), "Подарок не может быть null.");
            }
            _giftsList.Add(gift);
            return gift;
        }
        public Gift Update(int giftId, Gift gift)
        {
            if (giftId < 0 || giftId >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift), "Подарок не может быть null.");
            }
            _giftsList[giftId] = gift;
            return gift;
        }
        public Gift Delete(int giftId)
        {
            if (giftId < 0 || giftId >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }
            var deletedGift = _giftsList[giftId];
            _giftsList.RemoveAt(giftId);
            return deletedGift;
        }
        public void AddSweetToGift(int giftId, Sweet sweet, int count)
        {
            if (giftId < 0 || giftId >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }
            if (sweet == null)
            {
                throw new ArgumentNullException(nameof(sweet), "Добавляемая сладость не может быть null.");
            }
            if (count <= 0)
            {
                throw new ArgumentException("Количество добавляемых конфет не может быть меньше или равно null");
            }

            var gift = _giftsList[giftId];
            if (gift.Sweets.ContainsKey(sweet))
            {
                gift.Sweets[sweet] += count;
            }
            else
            {
                _giftsList[giftId].Sweets.Add(sweet, count);
            }
            
        }
        public Sweet FindSweetBySugarRange(int giftId, int startValue, int endValue)
        {
            if (giftId < 0 || giftId >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }

            if (startValue < -1 || startValue > endValue)
            {
                throw new ArgumentException("Неправильно задан диапазон.");
            }

            return _giftsList[giftId].Sweets
                .FirstOrDefault(s => s.Key.SugarWeight >= startValue && s.Key.SugarWeight <= endValue).Key;
        }
        public void Order(int giftId, SweetsOrderRule orderRule)
        {
            if (giftId < 0 || giftId >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }

            _giftsList[giftId].OrderBy(orderRule);
        }
    }
}
