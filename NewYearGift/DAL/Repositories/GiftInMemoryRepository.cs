using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.Domain.Models;

namespace NewYearGift.DAL.Repositories
{
    public class GiftInMemoryRepository : IGiftRepository
    {
        private readonly IDictionary<int, Gift> _giftsCollection;
        public GiftInMemoryRepository()
        {
            _giftsCollection = new Dictionary<int, Gift>();
        }
        public GiftInMemoryRepository(IDictionary<int, Gift> giftsCollection)
        {
            _giftsCollection = giftsCollection;
        }
        public Gift GetById(int giftId)
        {
            return _giftsCollection.ContainsKey(giftId) ? _giftsCollection[giftId] : null;
        }
        public IReadOnlyList<Gift> ListAll()
        {
            return _giftsCollection.Values.ToList();
        }
        public void Add(Gift gift)
        {
            int newId = GetNewId();
            gift.Id = newId;
            
            _giftsCollection.Add(gift.Id, gift);
        }
        public void Update(Gift gift)
        {
            int id = gift.Id;
            
            if (_giftsCollection.ContainsKey(id))
            {
                _giftsCollection[id] = gift;
            }
            else
            {
                _giftsCollection.Add(id, gift);
            }
        }
        public void Delete(Gift gift)
        {
            _giftsCollection.Remove(gift.Id);
        }
        
        private int GetNewId()
        {
            if (!_giftsCollection.Any())
            {
                return 1;
            }
            int lastId = _giftsCollection.Keys.Max();

            return ++lastId;
        }
    }
}
