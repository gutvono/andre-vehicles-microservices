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
using MongoDB.Driver;
using AndreVehicles.AddressApi.Utils;

namespace AndreVehicles.AddressApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AndreVehiclesContext _context;
        private readonly AddressService _addressService;
        private readonly IMongoCollection<Address> _mongoCollection;

        public AddressesController(AndreVehiclesContext context, AddressService addressService, IMongoConfig config)
        {
            _context = context;
            _addressService = addressService;

            var client = new MongoClient(config.ConnectionString);
            var db = client.GetDatabase(config.DatabaseName);
            _mongoCollection = db.GetCollection<Address>(config.AddressCollection);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAddress(int id, Address address)
        //{
        //    if (id != address.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(address).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AddressExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (string.IsNullOrEmpty(address.PostalCode))
            {
                return BadRequest("PostalCode is required.");
            }

            AddressViacep viacepAddress = await _addressService.GetViacepAddress(address.PostalCode);

            if (viacepAddress == null)
            {
                return BadRequest("Invalid PostalCode.");
            }

            var streetSplit = viacepAddress.Street.Split(" ");

            address.Neighborhood = viacepAddress.Neighborhood;
            address.City = viacepAddress.City;
            address.State = viacepAddress.State;
            address.StreetType = streetSplit[0];
            address.Street = string.Join(" ", streetSplit.Skip(1));

            _context.Address.Add(address);
            await _context.SaveChangesAsync();
            _mongoCollection.InsertOne(address);

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAddress(int id)
        //{
        //    var address = await _context.Address.FindAsync(id);
        //    if (address == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Address.Remove(address);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
