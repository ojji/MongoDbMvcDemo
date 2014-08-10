using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using RealEstate.Rentals;
using RealEstate.ViewModels;

namespace RealEstate.Controllers
{
    public class RentalsController : Controller
    {
        private readonly RealEstateContext _context = new RealEstateContext();

        public ActionResult Index(RentalsFilter filters)
        {
            var rentals = FilterRentals(filters);
            var model = new RentalsList() {Rentals = rentals, Filters = filters};
            return View(model);
        }

        private IEnumerable<Rental> FilterRentals(RentalsFilter filters)
        {
            IQueryable<Rental> rentals = _context.Rentals.AsQueryable().OrderBy(r => r.Price);

            if (filters.MinimumRooms.HasValue)
            {
                rentals = rentals.Where(r => r.NumberOfRooms >= filters.MinimumRooms.Value);
            }

            if (filters.PriceLimit.HasValue)
            {
                var query = Query<Rental>.LTE(r => r.Price, filters.PriceLimit.Value);
                rentals = rentals.Where(r => query.Inject());
            }

            return rentals;
        }

        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental postRental)
        {
            var rental = new Rental(postRental);
            _context.Rentals.Insert(rental);
            return RedirectToAction("Index");
        }

        public ActionResult AdjustPrice(string id)
        {
            Rental rental = GetRental(id);
            return View(rental);
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            Rental rental = GetRental(id);
            rental.AdjustPrice(adjustPrice);
            _context.Rentals.Save(rental);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _context.Rentals.Remove(Query.EQ("_id", new ObjectId(id)));
            return RedirectToAction("Index");
        }

        private Rental GetRental(string id)
        {
            return _context.Rentals.FindOneById(new ObjectId(id));
        }
    }
}