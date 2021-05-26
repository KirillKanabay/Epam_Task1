using System.Collections.Generic;
using NewYearGift.Models;
using NewYearGift.Services;

namespace NewYearGift.Controllers
{
    public class GiftController
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            _giftService = giftService;
        }

        public List<Gift> GetAll()
        {
            return _giftService.GetAll();
        }

        public Gift GetById(int id)
        {
            return _giftService.GetById(id);
        }

        public Gift Add(Gift gift)
        {
            return _giftService.Add(gift);
        }
        public Gift Update(int id, Gift gift)
        {
            return _giftService.Update(id, gift);
        }

        public Gift Delete(int id)
        {
            return _giftService.Delete(id);
        }
    }
}
