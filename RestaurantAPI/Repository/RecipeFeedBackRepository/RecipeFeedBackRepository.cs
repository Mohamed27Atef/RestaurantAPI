using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RecipeFeedBackRepository
{
    public class RecipeFeedBackRepository: IRecipeFeedBackRepository
    {
        private readonly RestaurantContext Context;
        public RecipeFeedBackRepository(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(RecipeFeedback entity)
        {
            Context.RecipeFeedbacks.Add(entity);
        }

        public void Delete(int id)
        {
            RecipeFeedback recipeFeedback = Context.RecipeFeedbacks.FirstOrDefault(r => r.id == id);
            Context.RecipeFeedbacks.Remove(recipeFeedback);
        }

        public List<RecipeFeedback> GetAll(string include = "")
        {
            var query = Context.RecipeFeedbacks.AsQueryable();
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

        public RecipeFeedback GetById(int id)
        {
            return Context.RecipeFeedbacks.FirstOrDefault(r => r.id == id);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(RecipeFeedback entity)
        {
            Context.RecipeFeedbacks.Update(entity);
        }

    }
}
