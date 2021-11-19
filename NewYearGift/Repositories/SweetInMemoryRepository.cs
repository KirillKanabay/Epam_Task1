using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewYearGift.Models;
using NewYearGift.Validators;

namespace NewYearGift.Repositories
{
    class SweetInMemoryRepository : ISweetRepository
    {
        private readonly IDictionary<int, Sweet> _sweetsCollection;
        private readonly IValidator<Sweet> _sweetValidator;
        public SweetInMemoryRepository()
        {
            _sweetsCollection = new Dictionary<int, Sweet>();
            _sweetValidator = new SweetValidator();
        }

        public SweetInMemoryRepository(IDictionary<int, Sweet> sweetsCollection)
        {
            _sweetValidator = new SweetValidator();
            _sweetsCollection = sweetsCollection;
        }

        public List<Sweet> GetAll()
        {
            return _sweetsCollection.Values.ToList();
        }

        void ISweetRepository.Add(Sweet item)
        {
            throw new NotImplementedException();
        }

        public Sweet GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentNullException(nameof(id), "Sweet id can't be less than 0");
            }

            return _sweetsCollection[id];
        }

        public IReadOnlyList<Sweet> ListAll()
        {
            return _sweetsCollection.Values.ToList();
        }

        public IReadOnlyList<Sweet> List(Func<Sweet, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate can't be null");
            }

            return _sweetsCollection.Values.Where(predicate).ToList();
        }

        public IReadOnlyList<Sweet> OrderBy(IComparer<Sweet> comparer)
        {
            return _sweetsCollection.Values.OrderBy(sweet => sweet, comparer).ToList();
        }
        
        public void Add(Sweet sweet)
        {
            _sweetValidator.Validate(sweet);
            
            _sweetsCollection.Add(sweet.Id, sweet);
        }

        public void Update(Sweet sweet)
        {
            _sweetValidator.Validate(sweet);

            int sweetId = sweet.Id;

            if (_sweetsCollection.ContainsKey(sweetId))
            {
                _sweetsCollection[sweetId] = sweet;
            }
            else
            {
                _sweetsCollection.Add(sweetId, sweet);
            }
        }

        public void Delete(int sweetId)
        {
            if (sweetId < 0)
            {
                throw new ArgumentException("Sweet's id can't be less than 0", nameof(sweetId));
            }
            
            if (!_sweetsCollection.ContainsKey(sweetId))
            {
                throw new ArgumentException($"Not found sweet with id: {sweetId}", nameof(sweetId));
            }

            _sweetsCollection.Remove(sweetId);
        }
    }
}
