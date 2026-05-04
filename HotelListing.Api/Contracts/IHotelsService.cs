using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.Contracts
{
    public interface IHotelsService
    {
        public Task<IEnumerable<GetHotelsDto>> GetHotelsAsync();
        public Task<GetHotelDto> GetHotelAsync(int id);
        public Task<GetHotelDto> CreateHotelAsync(CreateHotelDto hotelDto);
        public Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto);
        public Task DeleteHotelAsync(int id);
    }
}
