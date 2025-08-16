using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prabesh.Dtos
{
    public class NewRentalDto
    {
            public int CustomerId { get; set; }
            public List<int> MovieIds { get; set; }
    }
    public class TopRentals
    {
        public string CustomerName { get; set; }
        public DateTime DateRented { get; set; }
    }
}