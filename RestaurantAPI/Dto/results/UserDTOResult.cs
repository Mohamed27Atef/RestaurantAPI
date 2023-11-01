namespace RestaurantAPI.Dto
{
    public class UserDTOResult
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
        public string imageUrl {  get; set; }
    }
}
