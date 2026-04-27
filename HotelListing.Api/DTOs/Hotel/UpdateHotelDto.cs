using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Hotel
{
    public class UpdateHotelDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [Range(1, 5)]
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
