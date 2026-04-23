using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public IList<Hotel> Hotels { get; set; } = [];
    }
}
