using System.Collections.Generic;
using NewYearGift.Models;

namespace NewYearGift.Services
{
    public class GiftInMemoryService:IGiftService
    {
        private readonly List<Gift> _giftsList = new List<Gift>();
        public List<Gift> GetAll() => _giftsList;
        public Gift GetById(int id) => _giftsList[id];
        public Gift Add(Gift gift)
        {
            _giftsList.Add(gift);
            return gift;
        }
        public Gift Update(int id, Gift gift)
        {
            _giftsList[id] = gift;
            return gift;
        }
        public Gift Delete(int id)
        {
            var deletedGift = _giftsList[id];
            _giftsList.RemoveAt(id);
            return deletedGift;
        }
    }
}
