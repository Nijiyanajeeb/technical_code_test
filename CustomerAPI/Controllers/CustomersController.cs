using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> customers = new List<Customer>
        {
            //Hardcoded Data
            new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Phone = "1234567890" },
            new Customer { Id = 2, FirstName ="Stephen", LastName = "Smith", Email = "Stephen@example.com", Phone = "0012345678"}
        };

        // GET: api/customers
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
             if (customer != null)
            {
                if(customers.Any())
                {
                    // Assign a new ID
                    customer.Id = customers.Max(c => c.Id) + 1;
                }
                else
                {
                    //Assigning an ID if the list is empty
                    customer.Id = 1;
                }
                
                customers.Add(customer);
                return Ok(customer);
            }
           return BadRequest();
        }

        // PUT: api/customers/1
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Customer updatedCustomer)
        {
           
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;

            return Ok(customer);
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            customers.Remove(customer);
            return Ok();
        }
    }
}
