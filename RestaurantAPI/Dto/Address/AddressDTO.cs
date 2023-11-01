using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto.Address
{
    public class AddressDTO
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
