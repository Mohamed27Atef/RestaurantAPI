using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RecipeImageRespository
{
    public interface IRecipeImageRespository:IGenericRepository<RecipeImage>
    {
        public List<RecipeImage> GetAllByIdReceipe(int id);
        public RecipeImage GetByIdAndImgReceipe(int id, string img);
    }
}
