using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prabesh.Dtos
{
    public class CalculateTotalDto
    {
        public int CustomerId { get; set; }

        public List<int> MovieIds { get; set; }

        public decimal DiscountPercentage { get; set; }


        public decimal VAT { get; set; } = 0.05m;
    }
} 