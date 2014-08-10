using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RealEstate.ViewModels;

namespace RealEstate.Rentals
{
    public class Rental
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address = new List<string>();
        public List<PriceAdjustment> PriceAdjustments = new List<PriceAdjustment>();

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
        public string ImageId { get; set; }

        public Rental()
        {
            
        }

        public Rental(PostRental postRental)
        {
            Description = postRental.Description;
            NumberOfRooms = postRental.NumberOfRooms;
            Price = postRental.Price;
            Address = (postRental.Address ?? string.Empty).Split('\n').ToList();
        }


        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            var adjustment = new PriceAdjustment(Price, adjustPrice);
            PriceAdjustments.Add(adjustment);
            Price = adjustPrice.NewPrice;
        }

        public bool HasImage()
        {
            return !string.IsNullOrWhiteSpace(ImageId);
        }
    }
}