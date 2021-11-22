using System;
using System.Collections.Generic;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        public GiftService(IGiftRepository giftService)
        {
            _giftRepository = giftService;
        }

        public ServiceResponse<Gift> Add(Gift gift)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Gift> GetById(int giftId)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IEnumerable<Gift>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Gift> Update(Gift gift)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Gift> Delete(Gift gift)
        {
            throw new NotImplementedException();
        }
    }
}
