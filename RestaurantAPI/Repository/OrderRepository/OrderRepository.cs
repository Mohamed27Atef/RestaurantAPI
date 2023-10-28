using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Repository.OrderRepository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly RestaurantContext Context;
        public OrderRepository(RestaurantContext context)
        {
            Context = context;
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

        public List<Order> GetByLocation(string location)
        {
            return Context.Orders.Where(r => r.Location == location).ToList();
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
    }
}
