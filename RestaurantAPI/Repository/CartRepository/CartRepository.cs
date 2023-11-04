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
        public Cart getCartByUserId(int userId)
        {
            return Context.Carts.Where(u => u.userId == userId && u.OrderId == null).FirstOrDefault();
        }
        public Cart GetNonOrderedCartByUserId(int userId)
        {
            return Context.Carts.Where(u => (u.userId == userId && u.OrderId == null)).FirstOrDefault();
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

        public Cart getCatByOrderId(int orderId)
        {
            return Context.Carts.FirstOrDefault(r => r.OrderId == orderId);
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
