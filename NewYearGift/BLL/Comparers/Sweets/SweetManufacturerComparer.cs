using System;
using System.Collections.Generic;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Comparers.Sweets
{
    public class SweetManufacturerComparer : IComparer<Sweet>
    {
        public int Compare(Sweet sweet1, Sweet sweet2)
        {
            if (ReferenceEquals(sweet1, sweet2)) return 0;
            if (ReferenceEquals(null, sweet2)) return 1;
            if (ReferenceEquals(null, sweet1)) return -1;
            
            return string.Compare(sweet1.Manufacturer, sweet2.Manufacturer, StringComparison.CurrentCulture);
        }
    }
}