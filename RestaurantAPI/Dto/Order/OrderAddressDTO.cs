namespace RestaurantAPI.Dto.Order
{
    public class OrderAddressDTO
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
