using System.Collections.Generic;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Comparers.Sweets
{
    /// <summary>
    /// Компоратор конфет по весу
    /// </summary>
    public sealed class SweetWeightComparer : IComparer<Sweet>
    {
        public int Compare(Sweet sweet1, Sweet sweet2)
        {
            if (ReferenceEquals(sweet1, sweet2)) return 0;
            if (ReferenceEquals(null, sweet2)) return 1;
            if (ReferenceEquals(null, sweet1)) return -1;
            
            return sweet1.Weight.CompareTo(sweet2.Weight);
        }
    }
}