//using Microsoft.EntityFrameworkCore;
//using RestaurantAPI.Models;


//namespace RestaurantAPI.Repository
//{
//    public class CartUserRrepository : ICartUserRrepository
//    {
//        private readonly RestaurantContext context;

//        public CartUserRrepository(RestaurantContext context)
//        {
//            this.context = context;
//        }

//        public void Add(CartUser entity)
//        {
//            context.CartUsers.Add(entity);
//        }

//        public void Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public List<CartUser> GetAll(string include = "")
//        {
//            throw new NotImplementedException();
//        }

//        public CartUser GetById(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Cart getCartByUserId(int userId)
//        {
//            return context.CartUsers.Where(u => u.user_id == userId).Select(t => t.cart).FirstOrDefault();
//        }

//        public Cart GetNonOrderedCartByUserId(int userId)
//        {
//            return context.CartUsers.Include(u=>u.cart).Where(u =>(u.user_id == userId && u.cart.OrderId== null)).Select(t => t.cart).FirstOrDefault();
//        }

//        public int SaveChanges()
//        {
//            return context.SaveChanges();
//        }

//        public void Update(CartUser entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
