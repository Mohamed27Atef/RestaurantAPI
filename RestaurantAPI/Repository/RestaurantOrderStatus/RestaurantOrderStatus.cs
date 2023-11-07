using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RestaurantOrderStatus
{
    public class RestaurantOrderStatus : IRestaurantOrderStatus
    {
        private readonly RestaurantContext context;

        public RestaurantOrderStatus(RestaurantContext context)
        {
            this.context = context;
        }

        public void Add(RestaruantOrdersStatus entity)
        {
            context.RestaruantOrdersStatuses.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<RestaruantOrdersStatus> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public RestaruantOrdersStatus GetById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderStatus getStatusByRestaurntIdCartId(int restaurntId, int CartId)
        {
            return context.RestaruantOrdersStatuses.Where(r => r.restaurantId == restaurntId && r.cartId == CartId).Select(r => r.status).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(RestaruantOrdersStatus entity)
        {
            throw new NotImplementedException();
        }

        RestaruantOrdersStatus getRestaurntStatusOrder(int restaurntId, int CartId)
        {
            return context.RestaruantOrdersStatuses.Where(r => r.restaurantId == restaurntId && r.cartId == CartId).FirstOrDefault();
        }

        public void updateStatus(int restaurntId, int CartId, OrderStatus status)
        {
            var restaurantStatus = getRestaurntStatusOrder(restaurntId, CartId);
            restaurantStatus.status = status;
            context.RestaruantOrdersStatuses.Update(restaurantStatus);
        }
    }
}
