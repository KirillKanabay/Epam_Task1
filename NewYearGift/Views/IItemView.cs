namespace NewYearGift.Views
{
    public interface IItemView<out T> : IView
    {
        T SelectById(bool pause);
        void ShowAll(bool pause);
    }
}