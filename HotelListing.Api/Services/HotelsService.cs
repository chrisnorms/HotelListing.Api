using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Services
{
    public class HotelsService(HotelListingDbContext context) : IHotelsService
    {
        public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto hotelDto)
        {
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address,
                Rating = hotelDto.Rating,
                CountryId = hotelDto.CountryId,
            };

            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();

            return new GetHotelDto(hotel.Id, hotel.Name, hotel.Address, hotel.Rating, hotel.Country?.Name);
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
        }

        public async Task<GetHotelDto> GetHotelAsync(int id)
        {
            var hotel = await context.Hotels
                .Where(h => h.Id == id)
                .Select(h => new GetHotelDto(h.Id, h.Name, h.Address, h.Rating, h.Country!.ShortName))
                .FirstOrDefaultAsync();

            return hotel;
        }

        public async Task<IEnumerable<GetHotelsDto>> GetHotelsAsync()
        {
            var hotels = await context.Hotels
                .Select(h => new GetHotelsDto(h.Id, h.Name, h.Address, h.Rating, h.CountryId))
                .ToListAsync();

            return hotels;
        }

        public async Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto)
        {
            var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");
            hotel.Name = hotelDto.Name;
            hotel.Address = hotelDto.Address;
            hotel.Rating = hotelDto.Rating;
            hotel.CountryId = hotelDto.CountryId;

            context.Entry(hotel).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
