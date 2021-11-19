using System.Collections.Generic;
using NewYearGift.Models;

namespace NewYearGift.Comparers.SweetsComparers
{
    /// <summary>
    /// Компоратор конфет по количеству сахара
    /// </summary>
    public sealed class SweetSugarComparer : IComparer<Sweet>
    {
        public int Compare(Sweet sweet1, Sweet sweet2)
        {
            if (ReferenceEquals(sweet1, sweet2)) return 0;
            if (ReferenceEquals(null, sweet2)) return 1;
            if (ReferenceEquals(null, sweet1)) return -1;
            
            return sweet2.SugarWeight.CompareTo(sweet2.SugarWeight);
        }
    }
}