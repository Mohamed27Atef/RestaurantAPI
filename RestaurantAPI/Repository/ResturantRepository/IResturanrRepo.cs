using E_Commerce.Repository;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.ResturantRepository
{
    public interface IResturanrRepo:IGenericRepository<Resturant>
    {
        public Resturant getByAppId(string appId);
        public Resturant getByUserId(string UserId);
        public List<ResturantDto> getByAddress(string address);
        public IEnumerable<ResturantDto> getByCategoryId(int category_id);
        public List<ResturantDto> getByNameAndCategoryId(string name, int categoryId);
        public List<ResturantDto> getByNameAndLocation(string name, string locatoin);
        public List<ResturantDto> getByCategoryAndLocation(int cat_id, string locatoin);
        public List<ResturantDto> getByName(string name);
        public void UpdateIamge(string newUrl);
        List<string> getResaurantIamges(int restaruantId);
        List<ResturantDto> getByLocatoinAndCagegoryAndName(string q, int cat, string location);
        List<Table> getTaleRestaurant(int restaruantId);
        OpenCloseHours getOpenCloseHours(int restaurantId);


    }
}
