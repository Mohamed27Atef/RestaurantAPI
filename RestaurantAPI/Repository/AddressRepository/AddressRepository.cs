using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.AddressRepository
{
    public class AddressRepository: IAddressRepository
    {
        private readonly RestaurantContext context;

        public AddressRepository(RestaurantContext context)
        {
            this.context = context;
        }

        public void Add(Address entity)
        {
            context.Addresses.Add(entity);
        }

        public void Delete(int id)
        {
            Address address = GetById(id);
            context.Addresses.Remove(address);
        }

        public List<Address> GetAll(string include = "")
        {
            var query = context.Addresses.AsQueryable();
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

        public Address GetById(int id)
        {
            return context.Addresses.FirstOrDefault(c => c.Id == id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(Address entity)
        {
            context.Addresses.Update(entity);
        }
    }
}
