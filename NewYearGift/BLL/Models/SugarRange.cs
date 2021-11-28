namespace NewYearGift.BLL.Models
{
    public readonly struct SugarRange
    {
        public double MinWeight { get; }
        public double MaxWeight { get; }

        public SugarRange(double minWeight, double maxWeight)
        {
            MinWeight = minWeight;
            MaxWeight = maxWeight;
        }
    }
}