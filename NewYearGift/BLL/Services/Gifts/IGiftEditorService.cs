using System.Collections.Generic;
using NewYearGift.BLL.Enums;
using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Gifts
{
    public interface IGiftEditorService
    {
        ServiceResponse<GiftItem> GetById(Gift gift, int id);
        ServiceResponse<IEnumerable<GiftItem>> ListAll(Gift gift);
        ServiceResponse<IEnumerable<Sweet>> GetSweetsBySugarRange(Gift gift, SugarRange sugarRange);
        ServiceResponse<IEnumerable<GiftItem>> OrderSweetsInGift(Gift gift, SweetsOrderRule sweetsOrderRule);
        ServiceResponse<Gift> Add(Gift gift, GiftItem item);
        ServiceResponse<Gift> Update(Gift gift, GiftItem item);
        ServiceResponse<Gift> Delete(Gift gift, GiftItem item);
        ServiceResponse<int> SweetsCount(Gift gift);
        ServiceResponse<double> TotalWeight(Gift gift);
        ServiceResponse<decimal> TotalPrice(Gift gift);
    }
}