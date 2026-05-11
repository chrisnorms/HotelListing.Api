using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.MappingProfiles
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Hotel, GetHotelDto>()
                .ForMember(d => d.Country, cfg => cfg.MapFrom(s => s.Country!.Name));
            CreateMap<CreateHotelDto, Hotel>();
        }
    }
}
