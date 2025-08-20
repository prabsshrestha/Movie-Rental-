using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prabesh.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<MovieQuantityDto> Movies { get; set; }
    }
    public class MovieQuantityDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
    public class TopRentals
    {
        public string CustomerName { get; set; }
        public DateTime DateRented { get; set; }
    }

    public class LastMovieRentPerCustomer
    {
        public string Movie { get; set; }
        public DateTime DateRented { get; set; }
    }

    public class CustomerMostMovie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int total { get; set; }
    }
}