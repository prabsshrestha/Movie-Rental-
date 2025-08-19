using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prabesh.Models
{
    public class Min18YearsForRegistration: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var register = (RegisterViewModel)validationContext.ObjectInstance;

            if (register.Birthdate == null)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - register.Birthdate.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success :
                new ValidationResult("Customer should be atleast 18 years old to register");
        }
    }
}