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
using HotelListing.Api.Contracts;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController(ICountriesService countriesService) : ControllerBase
    {
        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
        {
            var countries = await countriesService.GetCountriesAsync();

            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
        {
            var country = await countriesService.GetCountryAsync(id);

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

            await countriesService.UpdateCountryAsync(id, countryDto);

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
        {
            var country = await countriesService.CreateCountryAsync(countryDto);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await countriesService.DeleteCountryAsync(id);

            return NoContent();
        }
    }
}
