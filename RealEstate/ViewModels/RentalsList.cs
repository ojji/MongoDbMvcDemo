using System.Collections.Generic;
using RealEstate.Rentals;

namespace RealEstate.ViewModels
{
    public class RentalsList
    {
        public IEnumerable<Rental> Rentals { get; set; }
        public RentalsFilter Filters { get; set; }
    }
}