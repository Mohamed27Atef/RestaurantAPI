namespace RestaurantAPI.Dto
{
    public class MostRated
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string imageUrl { get; set; }
        public decimal rate { get; set; }
    }
}
