using System.Collections.Generic;
using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Gifts
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