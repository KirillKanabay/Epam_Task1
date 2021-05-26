using System;
using System.Collections.Generic;
using NewYearGift.Models;

namespace NewYearGift.Services
{
    public class GiftInMemoryService:IGiftService
    {
        private readonly List<Gift> _giftsList = new List<Gift>();
        public List<Gift> GetAll() => _giftsList;

        public Gift GetById(int id)
        {
            if (id < 0 || id >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(id));
            }
            return _giftsList[id];
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
        public Gift Update(int id, Gift gift)
        {
            if (id < 0 || id >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(id));
            }
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift), "Подарок не может быть null.");
            }
            _giftsList[id] = gift;
            return gift;
        }
        public Gift Delete(int id)
        {
            if (id < 0 || id >= _giftsList.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(id));
            }
            var deletedGift = _giftsList[id];
            _giftsList.RemoveAt(id);
            return deletedGift;
        }
    }
}
