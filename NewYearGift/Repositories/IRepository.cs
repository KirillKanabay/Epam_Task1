using System;
using System.Collections.Generic;

namespace NewYearGift.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        T GetById(int id); 
        IEnumerable<T> ListAll();
        IEnumerable<T> List(Func<T, bool> predicate);
        IEnumerable<T> OrderBy(IComparer<T> comparer);
        void Update(int id, T item);
        void Delete(int id);
    }
}