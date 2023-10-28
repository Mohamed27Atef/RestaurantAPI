using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public class CartUserRrepository : ICartUserRrepository
    {
        private readonly RestaurantContext context;

        public CartUserRrepository(RestaurantContext context)
        {
            this.context = context;
        }

        public void Add(CartUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartUser> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public CartUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Cart getCartByUserId(int userId)
        {
            return context.CartUsers.Where(u => u.user_id == userId).Select(t => t.cart).FirstOrDefault();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(CartUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
