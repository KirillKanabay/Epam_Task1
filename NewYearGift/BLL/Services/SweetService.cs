using System;
using System.Collections.Generic;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;

namespace NewYearGift.BLL.Services
{
    public class SweetService
    {
        private readonly ISweetRepository _sweetService;

        public SweetService(ISweetRepository sweetService)
        {
            _sweetService = sweetService;
        }

        public List<Sweet> GetAll()
        {
            throw new NotImplementedException();
            //return _sweetService.GetAll();
        }

        public Sweet GetById(int id)
        {
            return _sweetService.GetById(id);
        }

        public Sweet Add(Sweet sweet)
        {
            throw new NotImplementedException();
            //return _sweetService.Add(sweet);
        }
        public Sweet Update(int id, Sweet sweet)
        {
            throw new NotImplementedException();
            //return _sweetService.Update(id, sweet);
        }

        public Sweet Delete(int id)
        {
            throw new NotImplementedException();
            // return _sweetService.Delete(id);
        }
    }
}
