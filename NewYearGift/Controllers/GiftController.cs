using System.Collections.Generic;
using NewYearGift.Models;
using NewYearGift.Services;

namespace NewYearGift.Controllers
{
    public class GiftController
    {
        private readonly IGiftService _giftService;
        private readonly ISweetService _sweetService;
        public GiftController(IGiftService giftService, ISweetService sweetService)
        {
            _giftService = giftService;
            _sweetService = sweetService;
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

        public void AddSweetToGift(int giftId, int sweetId, int count)
        {
            var sweet = _sweetService.GetById(sweetId);
            _giftService.AddSweetToGift(giftId, sweet, count);
        }

        public void Order(int giftId, SweetsOrderRule orderRule)
        {
            _giftService.Order(giftId, orderRule);
        }

        public Sweet FindSweetBySugarRange(int giftId, int startValue, int endValue)
        {
            return _giftService.FindSweetBySugarRange(giftId, startValue, endValue);
        }
    }
}
