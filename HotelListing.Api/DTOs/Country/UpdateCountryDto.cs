using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Country
{
    public class UpdateCountryDto
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2)]
        public string ShortName { get; set; }
    }
}
