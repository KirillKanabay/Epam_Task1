using System.Collections.Generic;
using NewYearGift.BLL.Models;
using NewYearGift.BLL.Services.Gifts;
using NewYearGift.BLL.Services.Sweets;
using NewYearGift.BLL.Services.Validation;
using NewYearGift.DAL.Repositories;
using NewYearGift.Domain.Models;
using NewYearGift.Views;

namespace NewYearGift
{
    class Program
    {
        private static readonly IDictionary<int, Sweet> SweetsCollection = new Dictionary<int, Sweet>()
        {
            {
                1,
                new ChocolateSweet()
                {
                    Id = 1,
                    Name = "Ромашка",
                    Manufacturer = "Коммунарка",
                    Weight = 10.4d,
                    SugarWeight = 4.2d,
                    Price = 0.5m,
                    KindOfChocolate = "Молочный",
                } 
            },
            {
              2,
              new ChocolateSweet()
              {
                  Id = 2,
                  Name = "Черемуха",
                  Manufacturer = "Спартак",
                  Weight = 12.4d,
                  SugarWeight = 3.5d,
                  Price = 0.45m,
                  KindOfChocolate = "Темный",
              } 
            },
            {
                3,
                new Lollipop()
                {
                    Id = 3,
                    Name = "Леденец",
                    Manufacturer = "Коммунарка",
                    Weight = 8d,
                    SugarWeight = 6d,
                    Price = 0.22m,
                    Flavor = "Дюшес",
                } 
            },
            {
                4,
                new Lollipop()
                {
                    Id = 4,
                    Name = "Леденец",
                    Manufacturer = "Коммунарка",
                    Weight = 8d,
                    SugarWeight = 6d,
                    Price = 0.22m,
                    Flavor = "Мята",
                } 
            },
            {
                5,
                new Lollipop()
                {
                    Id = 5,
                    Name = "Леденец",
                    Manufacturer = "Коммунарка",
                    Weight = 8d,
                    SugarWeight = 6d,
                    Price = 0.22m,
                    Flavor = "Барбарис",
                } 
            },
        };
        
        private static ISweetRepository _sweetRepository;
        private static IGiftRepository _giftRepository;
        
        private static IGiftService _giftService;
        private static IGiftEditorService _giftEditorService;
        private static ISweetService _sweetService;
        
        private static IValidationService<Gift> _giftValidationService;
        private static IValidationService<GiftItem> _giftItemValidationService;
        private static IValidationService<SugarRange> _sugarRangeValidationService;
        private static IValidationService<Sweet> _sweetValidationService;

        private static IView _mainView;
        private static IItemView<Gift> _giftView;
        private static IItemView<Sweet> _sweetView;
        
        private static void InitDependencies()
        {
            _giftRepository = new GiftInMemoryRepository();
            _sweetRepository = new SweetInMemoryRepository(SweetsCollection);

            _sweetValidationService = new SweetValidationService();
            _giftValidationService = new GiftValidationService();
            _giftItemValidationService = new GiftItemValidationService();
            _sugarRangeValidationService = new SugarRangeValidationService();
            
            _giftService = new GiftService(_giftRepository, _giftValidationService);
            _giftEditorService = new GiftEditorService(_sugarRangeValidationService, _giftItemValidationService);
            _sweetService = new SweetService(_sweetRepository, _sweetValidationService);

            _sweetView = new SweetView(_sweetService);
            _giftView = new GiftView(_giftService, _giftEditorService, _sweetView);
            _mainView = new MainView(_giftView, _sweetView);
        }

        static void Main()
        {
            InitDependencies();

            _mainView.Show();
        }
    }
}
