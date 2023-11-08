using E_Commerce.Repository;
using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Repository.RecipeFeedBackRepository
{
    public interface IRecipeFeedBackRepository : IGenericRepository<RecipeFeedback>
    {
        int GetNumberOfRecipeReview(int id);
        void Add(RecipeFeedback entity);
        void Delete(int id);
        List<RecipeFeedback> GetAll(string include = "");
        RecipeFeedback GetById(int id);
        int SaveChanges();
        void Update(RecipeFeedback entity);
        List<RecipeFeedback> GetReviewsForRecipe(int recipeId);
        public RecipeFeedback GetRecipeFeedbackByUserIdAndRecipeId(int userId, int recipeId);
    }
}
