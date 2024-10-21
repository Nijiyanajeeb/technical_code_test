using Microsoft.AspNetCore.Mvc;
using CustomerMVCApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System;

namespace CustomerMVCApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;
        public CustomerController()
        {
            // Using Uri class to define the API endpoint
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5001/api/customers");
        }

        // To retrieve the customer details to the Index page everytime.
        // GET: Customer/Index
        public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
            return View(customers);
        }
        else
        {
            return View(new List<Customer>());
        }
    }


        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // To create and add a new customer
        // POST: Customer/Create
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var response = await _client.PostAsJsonAsync("", customer);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index", new List<Customer>());
        }

        // To retrieve the details of the customer chosen to edit
        // GET: Customer/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var customer = await response.Content.ReadFromJsonAsync<Customer>();
                return View(customer);
            }
            return RedirectToAction("Index");
        }

        //To edit the details of an existing customer
        // POST: Customer/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            var response = await _client.PutAsJsonAsync($"customers/{customer.Id}", customer);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //To Delete a customer with a specific Id
        // GET: Customer/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    
    }
}
