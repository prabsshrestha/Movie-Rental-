using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Prabesh.Dtos;
using Prabesh.Models;

namespace Prabesh.Controllers.Api
{
    public class NewRentalsController : ApiController
    {

        private ApplicationDbContext _context;
    
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //to get the list of rentals 
        //Get /api/rentals
        [Route("api/rentals")]
        public IEnumerable<RentalDto> GetRentals()
        {
            var rentalsQuery = _context.Rentals
               .Include(c => c.Customer)
               .Include(m => m.Movie);

            return rentalsQuery
                .ToList()
                .Select(c => new RentalDto()
                {
                    Id = c.Id,
                    CustomerId = c.Customer.Id,
                    CustomerName = c.Customer.Name,
                    MovieId = c.Movie.Id,
                    MovieName = c.Movie.Name,
                    DateRented = c.DateRented
                });
        }

        [Route("api/totalrentals")]
        public IEnumerable<RentalDto> GetCustomerRentals()
        {
            var userId = User.Identity.GetUserId();
            var customer = _context.Customers.SingleOrDefault(c => c.UserId == userId);

            var rentalsquery = _context.Rentals
               .Include(r => r.Customer)
               .Include(r => r.Movie);

            return rentalsquery
               .Where(r => r.Customer.Id == customer.Id)
               .Select(r => new RentalDto
               {
                   Id = r.Id,
                   CustomerId = r.Customer.Id,
                   CustomerName = r.Customer.Name,
                   MovieId = r.Movie.Id,
                   MovieName = r.Movie.Name,
                   DateRented = r.DateRented
               })
               .ToList();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/rentals/{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteRental(int id)
        {
            var rentalInDb = _context.Rentals.SingleOrDefault(m => m.Id == id); 

            if (rentalInDb == null)
                return NotFound();
            _context.Rentals.Remove(rentalInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
