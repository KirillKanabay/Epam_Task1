using System;
using System.Collections.Generic;
using NewYearGift.Models;
using NewYearGift.Repositories;

namespace NewYearGift.Controllers
{
    public class SweetController
    {
        private readonly ISweetRepository _sweetService;

        public SweetController(ISweetRepository sweetService)
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
