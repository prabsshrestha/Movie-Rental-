using Prabesh.Dtos;
using Prabesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Prabesh.Controllers.Api
{
    public class CalculateTotalController : ApiController
    {
        private ApplicationDbContext _context;

        public CalculateTotalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CalculateTotal(CalculateTotalDto calculateTotal)
        {
            //checks if the movieIds is null or empty
            if (calculateTotal.MovieIds == null || !calculateTotal.MovieIds.Any())
                return BadRequest("No movies selected.");

            //fetching movies from the database on the basis of IDs
            var movies = _context.Movies.Where(m => calculateTotal.MovieIds.Contains(m.Id)).ToList();

            //if no movies are found based on the provided MovieIds
            if (!movies.Any())
                return NotFound();

            decimal totalAmount = (decimal)movies.Sum(m => m.Price);
            decimal discountPercentage = calculateTotal.DiscountPercentage;
            decimal VAT = calculateTotal.VAT;

            decimal discount = totalAmount *  (discountPercentage / 100);
            decimal discountedAmount = totalAmount - discount;

            decimal vatAmount = discountedAmount * (VAT / 100);
            decimal GrandTotalAmount = discountedAmount + vatAmount;

            return Ok(new { TotalAmount = GrandTotalAmount });

        }
    }
}
