using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prabesh.Dtos;
using Prabesh.Models;

namespace Prabesh.Controllers.Api
{
    public class CustomersController : ApiController
    {
        //first connect to the database and get the list of customers from the database
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //Action to get a list of customers
        //GET /api/customers

        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
              .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }


        //To get a single Customers
        //GET /api/Customers/1

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/Customers
        //add the new object to the database

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);


            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/Customers
        //updating the database, customer
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //checks if the clients send a valid id and if it exists in the database

            if (CustomerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, CustomerInDb);

            /* //update the customer

             CustomerInDb.Name = customerDto.Name;
             CustomerInDb.Birthdate = customerDto.Birthdate;
             CustomerInDb.IsSubscribedToNewletter = customerDto.IsSubscribedToNewletter;
             CustomerInDb.MembershipTypeId = customerDto.MembershipTypeId;*/

            // can use AutoMapper for large properties in the customer 
            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/Customers
        //deleting the existing customer from the database
        [HttpDelete]
        public void DeleteCustmer(int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //check if the customer exists in the database
            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //to delete the existing customer from the database
            _context.Customers.Remove(CustomerInDb);

            _context.SaveChanges();
        }
    }
}
