using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.RestaurantOrderStatus;
using RestaurantAPI.Services;
using System.Diagnostics;

namespace RestaurantAPI.Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantContext Context;

        public IRestaurantOrderStatus RestaurantOrderStatus { get; }

        public OrderRepository(RestaurantContext context, IRestaurantOrderStatus restaurantOrderStatus)
        {
            Context = context;
            RestaurantOrderStatus = restaurantOrderStatus;
        }

        public void Add(Order entity)
        {
            Context.Orders.Add(entity);
        }

        public void Delete(int id)
        {
            Order order = Context.Orders.FirstOrDefault(r => r.Id == id);
            Context.Orders.Remove(order);
        }

        public List<Order> GetAll(string include = "")
        {
            var query = Context.Orders.AsQueryable();
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

        public Order GetById(int id)
        {
            return Context.Orders.FirstOrDefault(r => r.Id == id);
        }

     
        public decimal GetOrderByIdTotalPrice(int id)
        {
            return Context.Orders.FirstOrDefault(r => r.Id == id).TotalPrice;

        }
        public decimal GetAllOrderTotalPrice() {
            var allOrder = Context.Orders.ToList();
            decimal allOrdersTotalPrice = 0;

            foreach (var order in allOrder)
                allOrdersTotalPrice += order.TotalPrice;

            return allOrdersTotalPrice;
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(Order entity)
        {
            Context.Orders.Update(entity);
        }

        public List<Order> getAllByUserId(int userId)
        {
            return Context.Orders.Where(r => r.UserId == userId).Include(or => or.Address).ToList();
        }

        public IEnumerable<Order> getOrderByReataurantId(int restaurantId)
        {
            var orders = Context.CartItems.Where(c => c.ResturantId == restaurantId).Include(r => r.Cart)
               .Include(r => r.Cart.order.Address).Include(r => r.Cart.order.User).ThenInclude(r => r.ApplicationUser).Select(r => r.Cart.order);
            return orders;
        }

        public int getStatusId(string status)
        {
        
            switch (status)
            {
                case "processed": return 0;
                case "shipped": return 1;
                case "enRoute": return 2;
                case "arrived": return 3;
                case "Canceled": return 4;
                default: return -1;
            }
        }

        public List<UserOrderByRestaurantIdDto> getOrderOfUsresByRestaurant( int userId, ICartItemRepository cartItemRepository)
        {
            IEnumerable<CartItem> restaurants = Context.CartItems
                .Where(r => r.Cart.userId == userId && r.Cart.OrderId != null).Include(r => r.Cart).ThenInclude(r => r.order).ThenInclude(r => r.Address).Include(r => r.Resturant);


            List<UserOrderByRestaurantIdDto> userOrdersRestaurant = new();
            var res = getAllCartOfUserdistincitByOrderId(restaurants).ToList();
            //var finalRes = getAllCartOfUserdistincitByRestaurantId(res).ToList();
            foreach (var item in res)
            {
                userOrdersRestaurant.Add(new UserOrderByRestaurantIdDto()
                {
                    restaurantName = item.Resturant.Name,
                    CreatedAt = item.Cart.order.CreatedAt,
                    city = item.Cart.order.Address.City,
                    country = item.Cart.order.Address.Country,
                    street = item.Cart.order.Address.Street,
                    Id = item.Cart.OrderId.Value,
                    restaurantId= item.ResturantId,
                    status = RestaurantOrderStatus.getStatusByRestaurntIdCartId(item.ResturantId, item.CartId).ToString(),
                    TotalPrice = cartItemRepository.getTotalPriceOrderByRestaurantIdAndOrderId(item.CartId, item.ResturantId)
                });

            }
            return userOrdersRestaurant;

        }

        public IEnumerable<CartItem> getAllCartOfUserdistincitByOrderId(IEnumerable<CartItem> s)
        {
            return s.DistinctBy(r => new { r.CartId, r.ResturantId });
        }

        public IEnumerable<CartItem> getAllCartOfUserdistincitByRestaurantId(IEnumerable<CartItem> s)
        {
            return s.DistinctBy(r => r.ResturantId);
        }

        public Address getAddressByOrderId(int id)
        {
            return Context.Orders.Where(r => r.Id == id).Select(r => r.Address).FirstOrDefault();
        }

        public List<int> createRestaruantOrderStatus(int CartId)
        {
            return Context.CartItems.Where(r => r.CartId == CartId).Select(r => r.ResturantId).Distinct().ToList();
        }

        public int getCartIdByOrderId(int orderId)
        {
            return Context.Carts.Where(r => r.OrderId == orderId).Select(r => r.id).FirstOrDefault();
        }
    }
}
