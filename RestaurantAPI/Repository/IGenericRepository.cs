namespace E_Commerce.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        T GetById(int id);
      
        List<T> GetAll(string include = "");

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);

        public int SaveChanges();

    }
}
