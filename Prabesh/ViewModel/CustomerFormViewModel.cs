using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prabesh.Models;

namespace Prabesh.ViewModel
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public string Title => Customer.Id != 0 ? "Edit Customer" : "New Customer";

    }
}
