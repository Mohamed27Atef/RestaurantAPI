using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Repository.OrderRepository
{
    public class OrderRepositorycs: IOrderRepository
    {
        private readonly RestaurantContext Context;
        public OrderRepositorycs(RestaurantContext context)
        {
            Context = context;
        }

        public void add(Order entity)
        {
            Context.Orders.Add(entity);
        }

        public void delete(int id)
        {
            Order order = Context.Orders.FirstOrDefault(r => r.Id == id);
            Context.Orders.Remove(order);
        }

        public List<Order> getAll(string include = "")
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

        public Order getById(int id)
        {
            return Context.Orders.FirstOrDefault(r => r.Id == id);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void update(Order entity)
        {
            Context.Orders.Update(entity);
        }
    }
}
