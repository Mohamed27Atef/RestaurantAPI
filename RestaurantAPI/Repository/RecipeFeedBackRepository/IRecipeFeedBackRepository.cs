using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RecipeFeedBackRepository
{
    public interface IRecipeFeedBackRepository:IGenericRepository<RecipeFeedback>
    {
        int getNumberOfRecipeReview(int id);

    }
}
