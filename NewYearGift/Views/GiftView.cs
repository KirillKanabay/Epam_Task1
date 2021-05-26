using System;
using NewYearGift.Controllers;

namespace NewYearGift.Views
{
    public class GiftView
    {
        private readonly GiftController _giftController;

        public GiftView(GiftController giftController)
        {
            _giftController = giftController;
        }

        public void Show()
        {
            Clear();
        }

        public void Clear()
        {
            Console.Clear();
            Console.WriteLine("==== Управление подарками. Введите help - для справки ====");
        }
    }
}
