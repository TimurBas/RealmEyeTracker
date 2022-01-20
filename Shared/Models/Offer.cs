namespace Shared.Models
{
    public class Offer
    {
        public string MainItemId { get; set; }
        public int SellQuantity { get; set; }
        public int BuyQuantity { get; set; }
        public string AddedTime { get; set; }
        public string OfferBy { get; set; }
        public string SecondaryItemId { get; set; }
    }
}