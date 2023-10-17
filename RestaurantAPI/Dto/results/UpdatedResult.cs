using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.results
{
    public class UpdatedResult : IResult
    {
        public Recipe reicpe { get; set; }
        Task IResult.ExecuteAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
