using RestaurantAPI.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public static class MapRestaurantToDtoService
    {
        public static ResturantDto mapResToDto(Resturant resturant)
        {
            var restaurantDto = new ResturantDto()
            {
                Address = resturant.Address,
                Cusinetype = resturant.Cusinetype,
                id = resturant.id,
                Image = resturant.Image,
                Latitude = resturant.Latitude,
                Longitude = resturant.Longitude,
                Name = resturant.Name,
                OpenHours = resturant.OpenHours,
                Rate = resturant.Rate
            };

            return restaurantDto;
        } 
    }
}
