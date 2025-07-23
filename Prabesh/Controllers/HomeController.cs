using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prabesh.Models;

namespace Prabesh.Controllers
{
    [AllowAnonymous] //make home page accessible to anonymous user 
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            ViewBag.CustomersCount = _context.Customers.Count();
            ViewBag.MoviesCount = _context.Movies.Count();
            ViewBag.RentalsCount = _context.Rentals.Count();

            return View();
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