using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.LocationRepository
{
    public class LocationRepository : ILocationRepository
    {

        private readonly RestaurantContext Context;
        public LocationRepository(RestaurantContext context)
        {
            Context = context;
        }




        public List<string> getAllLocatoin()
        {
            return Context.Resturants.Select(re => re.Address).Distinct().ToList();
        }
    }
}
