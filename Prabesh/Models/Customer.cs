using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Prabesh.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewletter { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        public MembershipType MembershipType { get; set; } //navigation property



        [Display(Name = "Date of Birth")]
        [Min18yearsIfAMember]
        public DateTime? Birthdate { get; set; }

        // New: link to AspNetUsers
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}