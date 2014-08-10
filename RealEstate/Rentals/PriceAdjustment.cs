using RealEstate.ViewModels;

namespace RealEstate.Rentals
{
    public class PriceAdjustment
    {
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Reason { get; set; }

        public PriceAdjustment(decimal oldPrice, AdjustPrice adjustPrice)
        {
            OldPrice = oldPrice;
            NewPrice = adjustPrice.NewPrice;
            Reason = adjustPrice.Reason;
        }

        public string Describe()
        {
            return string.Format("{0} -> {1} ({2})", OldPrice, NewPrice, Reason);
        }
    }
}