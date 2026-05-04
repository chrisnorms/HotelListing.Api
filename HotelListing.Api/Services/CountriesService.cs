using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Services
{
    public class CountriesService(HotelListingDbContext context) : ICountriesService
    {
        public async Task<IEnumerable<GetCountriesDto>> GetCountriesAsync()
        {
            var countries = await context.Countries
                    .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.ShortName))
                    .ToListAsync();

            return countries;
        }

        public async Task<GetCountryDto?> GetCountryAsync(int id)
        {
            var country = await context.Countries.Where(q => q.CountryId == id)
                    .Select(c => new GetCountryDto(
                        c.CountryId,
                        c.Name,
                        c.ShortName,
                        c.Hotels.Select(h => new GetHotelSlimDto(h.Id, h.Name, h.Address, h.Rating)).ToList()
                    ))
                    .FirstOrDefaultAsync();

            return country;
        }

        public async Task<GetCountryDto> CreateCountryAsync(CreateCountryDto countryDto)
        {
            var country = new Country
            {
                Name = countryDto.Name,
                ShortName = countryDto.ShortName,
            };

            //var resultDto = new GetCountryDto
            //{
            //    Id = country.CountryId,
            //    Name = country.Name,
            //    ShortName = country.ShortName,
            //    []
            //};

            context.Countries.Add(country);
            await context.SaveChangesAsync();

            return new GetCountryDto
            (
                country.CountryId,
                country.Name,
                country.ShortName,
                []
            );
        }

        public async Task UpdateCountryAsync(int id, UpdateCountryDto countryDto)
        {
            var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");

            country.Name = countryDto.Name;
            country.ShortName = countryDto.ShortName;

            context.Entry(country).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");
            context.Countries.Remove(country);
            await context.SaveChangesAsync();
        }
    }
}
