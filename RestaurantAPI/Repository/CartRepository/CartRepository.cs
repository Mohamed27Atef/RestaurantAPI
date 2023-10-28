using E_Commerce.Repository;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.CartRepository
{
    public class CartRepository: ICartRepository
    {
        private readonly RestaurantContext Context;
        public CartRepository(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(Cart entity)
        {
            Context.Carts.Add(entity);
        }

        public void Delete(int id)
        {
            Cart cart = Context.Carts.FirstOrDefault(r => r.id == id);
            Context.Carts.Remove(cart);
        }

        public List<Cart> GetAll(string include = "")
        {
            var query = Context.Carts.AsQueryable();
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

        public Cart GetById(int id)
        {
            return Context.Carts.FirstOrDefault(r => r.id == id);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(Cart entity)
        {
            Context.Carts.Update(entity);
        }

      
    }
}
