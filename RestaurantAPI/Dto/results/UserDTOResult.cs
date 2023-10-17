namespace RestaurantAPI.Dto
{
    public class UserDTOResult: IResult
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }


        /////////////////////////////////////////////////
        public Task ExecuteAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
    }
}
