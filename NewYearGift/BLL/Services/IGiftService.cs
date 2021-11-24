using System;
using System.Collections.Generic;
using NewYearGift.BLL.Enums;
using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;
using NewYearGift.Models;

namespace NewYearGift.BLL.Services
{
    public interface IGiftService
    {
        ServiceResponse<Gift> Add(Gift gift);
        ServiceResponse<Gift> GetById(int giftId);
        ServiceResponse<IEnumerable<Gift>> ListAll();
        ServiceResponse<Gift> Update(Gift gift);
        ServiceResponse<Gift> Delete(Gift gift);
    }
}