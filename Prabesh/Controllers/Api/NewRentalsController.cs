using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
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
                    MovieName = c.Movie.Name
                });
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
    }
}
