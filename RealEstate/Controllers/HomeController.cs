using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstate.Rentals;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly RealEstateContext _dbContext = new RealEstateContext();

        public ActionResult Index()
        {
            _dbContext.Database.GetStats();
            return Json(_dbContext.Database.Server.BuildInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}