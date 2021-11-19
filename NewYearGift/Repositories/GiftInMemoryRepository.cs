using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGift.Models;
using NewYearGift.Validators;

namespace NewYearGift.Repositories
{
    public class GiftInMemoryRepository : IGiftRepository
    {
        private readonly IDictionary<int, Gift> _giftsCollection;
        private readonly IValidator<Gift> _giftValidator;
        public GiftInMemoryRepository()
        {
            _giftValidator = new GiftValidator();
        }
        public GiftInMemoryRepository(IDictionary<int, Gift> giftsCollection)
        {
            _giftsCollection = giftsCollection;
        }
        
        public Gift GetById(int giftId)
        {
            if (giftId < 0)
            {
                throw new ArgumentException("Gift's id cannot be null", nameof(giftId));
            }

            return _giftsCollection[giftId];
        }
        public IReadOnlyList<Gift> ListAll()
        {
            return _giftsCollection.Values.ToList();
        }

        public IReadOnlyList<Gift> List(Func<Gift, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate can't be null");
            }
            
            return _giftsCollection.Values.Where(predicate).ToList();
        }

        public void Add(Gift gift)
        {
            _giftValidator.Validate(gift);
            
            _giftsCollection.Add(gift.Id, gift);
        }
        public void Update(Gift gift)
        {
            _giftValidator.Validate(gift);
            
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
        public void Delete(int giftId)
        {
            if (giftId < 0)
            {
                throw new ArgumentException("Gift's id can't be less than 0", nameof(giftId));
            }
            
            _giftsCollection.Remove(giftId);
        }

        public IReadOnlyList<Gift> OrderBy(IComparer<Gift> comparer)
        {
            return _giftsCollection.Values.OrderBy(x => x, comparer).ToList();
        }
    }
}
