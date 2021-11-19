using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGift.Models;

namespace NewYearGift.Repositories
{
    public class GiftInMemoryRepository : IGiftRepository
    {
        private readonly IList<Gift> _giftsCollection;

        public GiftInMemoryRepository()
        {
            
        }
        public GiftInMemoryRepository(IList<Gift> giftsCollection)
        {
            _giftsCollection = giftsCollection;
        }
        
        public Gift GetById(int giftId)
        {
            if (giftId < 0)
            {
                throw new ArgumentException("Gift id cannot be null", nameof(giftId));
            }
            
            //todo: Возвратить элемент по id
            throw new NotImplementedException();
            // return _giftsCollection.Where(gift => gift.);
        }
        public IEnumerable<Gift> ListAll()
        {
            return _giftsCollection;
        }

        public IEnumerable<Gift> List(Func<Gift, bool> predicate)
        {
            return _giftsCollection.Where(predicate);
        }

        public void Add(Gift gift)
        {
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift), "Подарок не может быть null.");
            }
            _giftsCollection.Add(gift);
        }
        public void Update(int giftId, Gift gift)
        {
            if (giftId < 0 || giftId >= _giftsCollection.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift), "Подарок не может быть null.");
            }
            _giftsCollection[giftId] = gift;
        }
        public void Delete(int giftId)
        {
            if (giftId < 0 || giftId >= _giftsCollection.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }
            
            var deletedGift = _giftsCollection[giftId];
            _giftsCollection.RemoveAt(giftId);
        }
        public void AddSweetToGift(int giftId, Sweet sweet, int count)
        {
            if (giftId < 0 || giftId >= _giftsCollection.Count)
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

            var gift = _giftsCollection[giftId];
            if (gift.Sweets.ContainsKey(sweet))
            {
                gift.Sweets[sweet] += count;
            }
            else
            {
                _giftsCollection[giftId].Sweets.Add(sweet, count);
            }
            
        }
        public Sweet FindSweetBySugarRange(int giftId, int startValue, int endValue)
        {
            if (giftId < 0 || giftId >= _giftsCollection.Count)
            {
                throw new ArgumentException("Такого подарка не существует.", nameof(giftId));
            }

            if (startValue < -1 || startValue > endValue)
            {
                throw new ArgumentException("Неправильно задан диапазон.");
            }

            return _giftsCollection[giftId].Sweets
                .FirstOrDefault(s => s.Key.SugarWeight >= startValue && s.Key.SugarWeight <= endValue).Key;
        }

        public void Order(int giftId, SweetsOrderRule orderRule)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gift> OrderBy(IComparer<Gift> comparer)
        {
            return _giftsCollection.OrderBy(x => x, comparer);
        }
    }
}
