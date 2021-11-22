using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGift.BLL.Services.Validation;
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

        public Sweet GetById(int id)
        { 
            return _sweetsCollection[id];
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

        public void Delete(int sweetId)
        {
            _sweetsCollection.Remove(sweetId);
        }
        
        private int GetNewId()
        {
            int lastId = _sweetsCollection.Keys.Max();

            return lastId == 0 ? 0 : ++lastId;
        }
    }
}
