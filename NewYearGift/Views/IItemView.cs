namespace NewYearGift.Views
{
    public interface IItemView<out T> : IView
    {
        T SelectById();
        void ShowAll();
    }
}