using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.ResturantFeedBackRepository
{
    public interface IResturantFeedBackRepository:IGenericRepository<ResturantFeedback>
    {
        List<ResturantFeedback> GetReviewsForRestaurant(int restaurantId);
        public ResturantFeedback GetRestaurantFeedbackByUserIdAndRestaurantId(int userId, int restaurantId);

    }
}
