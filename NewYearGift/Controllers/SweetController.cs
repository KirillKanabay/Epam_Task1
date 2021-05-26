using System.Collections.Generic;
using NewYearGift.Models;
using NewYearGift.Services;

namespace NewYearGift.Controllers
{
    public class SweetController
    {
        private readonly ISweetService _sweetService;

        public SweetController(ISweetService sweetService)
        {
            _sweetService = sweetService;
        }

        public List<Sweet> GetAll()
        {
            return _sweetService.GetAll();
        }

        public Sweet GetById(int id)
        {
            return _sweetService.GetById(id);
        }

        public Sweet Add(Sweet sweet)
        {
            return _sweetService.Add(sweet);
        }
        public Sweet Update(int id, Sweet sweet)
        {
            return _sweetService.Update(id, sweet);
        }

        public Sweet Delete(int id)
        {
            return _sweetService.Delete(id);
        }
    }
}
