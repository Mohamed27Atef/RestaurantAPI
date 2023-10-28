using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly RestaurantContext context;

        public CartItemRepository(RestaurantContext context)
        {
            this.context = context;
        }

        public void Add(CartItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetAllByCartId(int cartId)
        {
            return context.CartItems.Where(r => r.CartId == cartId).Include(r => r.Resturant).Include(r => r.Recipe).ToList();
        }

        public CartItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(CartItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
