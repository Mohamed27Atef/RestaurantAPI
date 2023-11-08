using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Repository.RecipeFeedBackRepository
{
    public class RecipeFeedBackRepository : IRecipeFeedBackRepository
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

        public int GetNumberOfRecipeReview(int id)
        {
            return Context.RecipeFeedbacks.Where(r => r.RecipeId == id).Count();
        }

        public void Delete(int id)
        {
            RecipeFeedback recipeFeedback = Context.RecipeFeedbacks.FirstOrDefault(r => r.id == id);
            if (recipeFeedback != null)
            {
                Context.RecipeFeedbacks.Remove(recipeFeedback);
            }
        }

        public List<RecipeFeedback> GetAll(string include = "")
        {
            var query = Context.RecipeFeedbacks.AsQueryable();
            if (!string.IsNullOrEmpty(include))
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
        public RecipeFeedback GetRecipeFeedbackByUserIdAndRecipeId(int userId, int recipeId)
        {
            return Context.RecipeFeedbacks.FirstOrDefault(r => r.userId == userId && r.RecipeId==recipeId);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(RecipeFeedback entity)
        {
            Context.RecipeFeedbacks.Update(entity);
        }

        public List<RecipeFeedback> GetReviewsForRecipe(int recipeId)
        {
            return Context.RecipeFeedbacks
                .Where(r => r.RecipeId == recipeId)
                .ToList();
        }
    }
}
