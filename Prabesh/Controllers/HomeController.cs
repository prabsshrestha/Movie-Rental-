using System;
using System.Collections.Generic;
using System.Linq;
using Prabesh.Repository;
using System.Web.Mvc;
using Prabesh.Models;
using Prabesh.Dtos;
using System.Threading.Tasks;

namespace Prabesh.Controllers
{
    [AllowAnonymous] 
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        private readonly ContactService _contactService;
        public HomeController()
        {
            _context = new ApplicationDbContext();
            _contactService = new ContactService();
        }

        public ActionResult Index()
        {
            ViewBag.CustomersCount = _context.Customers.Count();
            ViewBag.MoviesCount = _context.Movies.Count();
            ViewBag.RentalsCount = _context.Rentals.Count();
            ViewBag.TopCustomers = GetTopCustomers();
            ViewBag.TopMovies = GetTopMovie();
            ViewBag.TopRentals = GetTopRentals();
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

        private List<TopCustomerDto> GetTopCustomers()
        {
            return _context.Rentals
                .GroupBy(c => new { c.Customer.Id, c.Customer.Name })
                .Select(g => new TopCustomerDto
                {
                    CustomerId = g.Key.Id,
                    CustomerName = g.Key.Name,
                    TotalRentals = g.Count()
                })
                .OrderByDescending(g => g.TotalRentals)
                .Take(3)
                .ToList();
        }

        private List<TopMovie> GetTopMovie()
        {
            return _context.Rentals
                .GroupBy(c => new { c.Movie.Id, c.Movie.Name })
                .Select(g => new TopMovie
                {
                    MovieId = g.Key.Id,
                    MovieName = g.Key.Name,
                    total = g.Count()
                })
                .OrderByDescending(g => g.total)
                .Take(3)
                .ToList();
        }

        private List<TopRentals> GetTopRentals()
        {
            return _context.Rentals
                .GroupBy(r => r.Customer.Id)
                .Select(g => g.OrderByDescending(r => r.DateRented).FirstOrDefault())
                .OrderByDescending(r => r.DateRented)
                .Take(2)
                .Select(r => new TopRentals
                {
                    DateRented = r.DateRented,
                    CustomerName = r.Customer.Name
                })
                .ToList();
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(Contact model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid form submission.";
                return RedirectToAction("Contact");
            }

            var result = await _contactService.SendMessageAsync(model);

            if (result.IsSuccess)
                TempData["success"] = "Your message has been sent successfully.";
            else
                TempData["error"] = $"Error sending message: {result.ErrorMessage}";

            return RedirectToAction("Index");
        }
    }
}