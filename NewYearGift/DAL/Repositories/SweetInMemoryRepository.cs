using System.Collections.Generic;
using System.Linq;
using NewYearGift.Domain.Models;

namespace NewYearGift.DAL.Repositories
{
    class SweetInMemoryRepository : ISweetRepository
    {
        private readonly IDictionary<int, Sweet> _sweetsCollection;
        public SweetInMemoryRepository()
        {
            _sweetsCollection = new Dictionary<int, Sweet>();
        }

        public SweetInMemoryRepository(IDictionary<int, Sweet> sweetsCollection)
        {
            _sweetsCollection = sweetsCollection;
        }
        
        public List<Sweet> GetAll()
        {
            return _sweetsCollection.Values.ToList();
        }
        
        public void Add(Sweet sweet)
        {
            int newId = GetNewId();
            sweet.Id = newId;
            
            _sweetsCollection.Add(sweet.Id, sweet);
        }

        public Sweet GetById(int sweetId)
        { 
            return _sweetsCollection.ContainsKey(sweetId) ? _sweetsCollection[sweetId] : null;
        }

        public IReadOnlyList<Sweet> ListAll()
        {
            return _sweetsCollection.Values.ToList();
        }
        
        public void Update(Sweet sweet)
        {
            int sweetId = sweet.Id;

            if (_sweetsCollection.ContainsKey(sweetId))
            {
                _sweetsCollection[sweetId] = sweet;
            }
            else
            {
                int newId = GetNewId();
                sweet.Id = newId;
                _sweetsCollection.Add(sweetId, sweet);
            }
        }

        public void Delete(Sweet sweet)
        {
            _sweetsCollection.Remove(sweet.Id);
        }
        
        private int GetNewId()
        {
            if (!_sweetsCollection.Any())
            {
                return 1;
            }
            int lastId = _sweetsCollection.Keys.Max();

            return ++lastId;
        }
    }
}
