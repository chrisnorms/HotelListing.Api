using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;

        public CountriesController(HotelListingDbContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
        {
            var countries = await _context.Countries
                .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.ShortName))
                .ToListAsync();

            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
        {
            var country = await _context.Countries.Where(q => q.CountryId == id)
                .Select(c => new GetCountryDto(
                    c.CountryId, 
                    c.Name, 
                    c.ShortName,
                    c.Hotels.Select(h => new GetHotelSlimDto(h.Id, h.Name, h.Address, h.Rating)).ToList()
                ))
                .FirstOrDefaultAsync();

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto countryDto)
        {
            if (id != countryDto.CountryId)
            {
                return BadRequest();
            }

            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                NotFound();
            }

            country.Name = countryDto.Name;
            country.ShortName = countryDto.ShortName;

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
        {
            var country = new Country
            {
                Name = countryDto.Name,
                ShortName = countryDto.ShortName,
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            //var resultDto = new GetCountryDto
            //{
            //    Id = country.CountryId,
            //    Name = country.Name,
            //    ShortName = country.ShortName,
            //    []
            //};

            return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CountryExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(e => e.CountryId == id);
        }
    }
}
