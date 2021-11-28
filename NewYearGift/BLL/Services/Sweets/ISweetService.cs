using System.Collections.Generic;
using NewYearGift.BLL.Models;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services.Sweets
{
    public interface ISweetService
    {
        ServiceResponse<Sweet> Add(Sweet sweet);
        ServiceResponse<Sweet> GetById(int sweetId);
        ServiceResponse<IEnumerable<Sweet>> ListAll();
        ServiceResponse<Sweet> Update(Sweet sweet);
        ServiceResponse<Sweet> Delete(Sweet sweet);
    }
}