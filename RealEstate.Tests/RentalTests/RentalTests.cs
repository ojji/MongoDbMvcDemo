using MongoDB.Bson;
using NUnit.Framework;
using RealEstate.Rentals;

namespace RealEstate.Tests.RentalTests
{
    [TestFixture]
    public class RentalTests : AssertionHelper
    {
        [Test]
        public void RentalModel_Price_Is_Represented_As_Double_In_BsonDocument()
        {
            var rental = new Rental() {Price = 15m};
            var rentalDocument = rental.ToBsonDocument();

            Expect(rentalDocument["Price"].IsDouble, True);
        }

        [Test]
        public void RentalModel_Id_Is_Represented_As_An_ObjectId()
        {
            var rental = new Rental();
            rental.Id = ObjectId.GenerateNewId().ToString();
            var rentalDocument = rental.ToBsonDocument();

            Expect(rentalDocument["_id"].BsonType, Is.EqualTo(BsonType.ObjectId));
        }
      
    }
}