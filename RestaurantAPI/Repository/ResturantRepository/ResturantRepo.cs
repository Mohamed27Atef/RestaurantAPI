using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Repository.ResturantRepository
{
    public class ResturantRepo : IResturanrRepo
    {

        private readonly RestaurantContext Context;
        public ResturantRepo(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(Resturant entity)
        {
            Context.Resturants.Add(entity);
        }

        public void Delete(int id)
        {
            Resturant res = Context.Resturants.FirstOrDefault(r => r.id == id);
            Context.Resturants.Remove(res);
        }

        public List<Resturant> GetAll(string include = "")
        {
            var query = Context.Resturants.AsQueryable();
            if (!String.IsNullOrEmpty(include))
            {
                var includes = include.Split(",");
                foreach (var inc in includes)
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.ToList();
        }
        public Resturant getByAppId(string appId)
        {
            return this.Context.Resturants.Include(r=> r.Cateigories).FirstOrDefault(r => r.ApplicationIdentityUserID == appId);
        }

        public List<ResturantDto> getByAddress(string address)
        {
            if(address == "0")
                return Context.Resturants.Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
            return Context.Resturants.Where(r => r.Address == address).Select(r => 
            new ResturantDto() {
                Address= r.Address,
                Cusinetype = r.Cusinetype,
                id = r.id,
                Image = r.Image,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                Name = r.Name,
                OpenHours = r.OpenHours,
                Rate = r.Rate
            }).ToList();

        }

        public IEnumerable<ResturantDto> getByCategoryId(int category_id)
        {
            if(category_id == 0)
                return Context.Resturants.Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
            return Context.RestaurantCateigories.Where(ca => ca.CategoryId == category_id).Include(c => c.Resturant).Select(t => MapRestaurantToDtoService.mapResToDto(t.Resturant)).ToList();
            
        }

        public Resturant GetById(int id)
        {
            return Context.Resturants.Where(r => r.id == id)
                //.Include(r => r.Recipes)
                //.Include(r => r.resturantFeedbacks)
                .Include(r => r.ClosingDays)
                //.Include(r => r.RestaurantImages)
                .FirstOrDefault();
        }

        public List<string> getResaurantIamges(int restaruantId)
        {
            return Context.RestaurantImages.Where(r => r.restaurantId == restaruantId).Select(r => r.imageUrl).ToList();
        }


        public Resturant getByUserId(string UserId)
        {
            return Context.Resturants.FirstOrDefault(r=> r.ApplicationIdentityUserID == UserId);
        }

        public List<ResturantDto> getByName(string name)
        {
            return Context.Resturants.Where(res => res.Name.Contains(name)).Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
        }

        public List<ResturantDto> getByNameAndCategoryId(string name, int categoryId)
        {
          if (categoryId == 0)
                return Context.Resturants.Where(res => res.Name.Contains(name)).Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
            return getByCategoryId(categoryId).Where(res => res.Name.Contains(name)).ToList();

        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(Resturant entity)
        {
            Context.Resturants.Update(entity);
        }

        public void UpdateIamge(string newUrl)
        {
            throw new NotImplementedException();
        }

        public List<ResturantDto> getByLocatoinAndCagegoryAndName(string q, int cat, string location)
        {
            if (location == "0")
                return getByNameAndCategoryId(q, cat);
            else if (cat == 0)
                return getByNameAndLocation(q, location);
            else if (q == "")
                return getByCategoryAndLocation(cat, location);
            else
                return getByCategoryId(0).ToList();

        }

        public List<ResturantDto> getByNameAndLocation(string name, string locatoin)
        {
            if (name == "")
                getByAddress(locatoin);
            return Context.Resturants.Where(res => res.Name.Contains(name)).Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
        }

        public List<ResturantDto> getByCategoryAndLocation(int cat_id, string locatoin)
        {
            return getByCategoryId(cat_id).Where(res => res.Address == locatoin).ToList();
        }

        public List<Table> getTaleRestaurant(int restaruantId)
        {
            return Context.Tables.Where(t => t.ResturantId == restaruantId).ToList();
        }

        public OpenCloseHours getOpenCloseHours(int restaurantId)
        {
            return Context.Resturants.Where(r => r.id == restaurantId).Select(r => new OpenCloseHours() { closeHours = r.ClosingHours, openHours = r.OpenHours }).FirstOrDefault();
        }
    }
}
