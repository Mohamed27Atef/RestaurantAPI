using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly RestaurantContext Context;
        public ProductRepository(RestaurantContext context)
        {
            Context = context;
        }


        public void add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> getAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public Product getByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Product getById(int id)
        {
            throw new NotImplementedException();
        }

        public Product getByRestaurantId(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
