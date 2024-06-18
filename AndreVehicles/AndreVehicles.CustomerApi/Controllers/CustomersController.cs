using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreVehicles.Data;
using Models;
using AndreVehicles.AddressApi.Services;
using System.Text;
using Newtonsoft.Json;

namespace AndreVehicles.CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AndreVehiclesContext _context;

        public CustomersController(AndreVehiclesContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer() => await _context.Customer.Include(c => c.Address).ToListAsync();

        [HttpGet("{document}")]
        public async Task<ActionResult<Customer>> GetCustomer(string document)
        {
            var customer = await _context.Customer.Where(c => c.Document == document).Include(c => c.Address).FirstOrDefaultAsync();

            if (customer == null) return NotFound();

            return customer;
        }

        //[HttpPut("{document}")]
        //public async Task<IActionResult> PutCustomer(string document, CustomerDTO customerDTO)
        //{
        //    if (document != customerDTO.Document) return BadRequest();

        //    _context.Entry(customerDTO).State = EntityState.Modified;

        //    try { await _context.SaveChangesAsync(); }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CustomerExists(document)) { return NotFound(); }
        //        else { throw; }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);

            using (HttpClient client = new HttpClient())
            {
                string addressApiUrl = "https://localhost:7273/api/addresses";
                string jsonCustomerAddress = JsonConvert.SerializeObject(customer.Address);
                StringContent content = new StringContent(jsonCustomerAddress, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(addressApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    Address viacepAddress = JsonConvert.DeserializeObject<Address>(jsonResponse);
                    var streetSplit = viacepAddress.Street.Split(" ");

                    customer.Address.Neighborhood = viacepAddress.Neighborhood;
                    customer.Address.City = viacepAddress.City;
                    customer.Address.State = viacepAddress.State;
                    customer.Address.StreetType = streetSplit[0];
                    customer.Address.Street = string.Join(" ", streetSplit.Skip(1));
                }
                else { return StatusCode((int)response.StatusCode, "Erro ao enviar requisição para a API Address."); }
            }

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Document)) { return Conflict(); }
                else { throw; }
            }

            return CreatedAtAction("GetCustomer", new { document = customer.Document }, customer);
        }

        //[HttpDelete("{document}")]
        //public async Task<IActionResult> DeleteCustomer(string document)
        //{
        //    var customer = await _context.Customer.FindAsync(document);
        //    if (customer == null) { return NotFound(); }

        //    _context.Customer.Remove(customer);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CustomerExists(string document) => _context.Customer.Any(e => e.Document == document);
    }
}
