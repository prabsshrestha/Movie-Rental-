using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prabesh.Dtos
{
    public class TopCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int TotalRentals { get; set; }
    }
}