using E_Commerce.Repository;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.ResturantFeedBackRepository
{
    public class ResturantFeedBackRepository: IResturantFeedBackRepository
    {
        private readonly RestaurantContext Context;
        public ResturantFeedBackRepository(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(ResturantFeedback entity)
        {
            Context.ResturantFeedbacks.Add(entity);
        }

       
        public void Delete(int id)
        {
            ResturantFeedback resturantFeedback = Context.ResturantFeedbacks.FirstOrDefault(r => r.id == id);
            Context.ResturantFeedbacks.Remove(resturantFeedback);
        }

        public List<ResturantFeedback> GetAll(string include = "")
        {
            var query = Context.ResturantFeedbacks.AsQueryable();
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

        public ResturantFeedback GetById(int id)
        {
            return Context.ResturantFeedbacks.FirstOrDefault(r => r.id == id);
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public void Update(ResturantFeedback entity)
        {
            Context.ResturantFeedbacks.Update(entity);
        }


        public List<ResturantFeedback> GetReviewsForRestaurant(int restaurantId)
        {
            return Context.ResturantFeedbacks
                .Where(feedback => feedback.ResturantId == restaurantId).Include(r => r.User).ThenInclude(r => r.ApplicationUser)
                .ToList();
        }

        public ResturantFeedback GetRestaurantFeedbackByUserIdAndRestaurantId(int userId, int restaurantId)
        {
            return Context.ResturantFeedbacks.FirstOrDefault(r => r.UserId == userId && r.ResturantId == restaurantId);
        }

    }
}
