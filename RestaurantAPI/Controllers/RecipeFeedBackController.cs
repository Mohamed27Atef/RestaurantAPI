using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.RecipeFeedBack;
using System.Security.Claims;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.RecipeFeedBackRepository;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Repository.CartRepository;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeFeedbackController : BaseApiClass
    {
        private readonly IRecipeFeedBackRepository _recipeFeedbackRepository;
        private readonly IUserRepository iUserRepository;
        private readonly IUserRepository userRepository;

        public RecipeFeedbackController(
            IRecipeFeedBackRepository recipeFeedbackRepository,
            IUserRepository iUserRepository,
            IUserRepository userRepository)
        {
            _recipeFeedbackRepository = recipeFeedbackRepository;
            this.iUserRepository = iUserRepository;
            this.userRepository = userRepository;
        }

        [HttpGet("recipe/{recipeId}")]
        public ActionResult<IEnumerable<RecipeFeedbackDto>> GetReviewsForRecipe(int recipeId)
        {
            var recipeReviews = _recipeFeedbackRepository.GetReviewsForRecipe(recipeId);

            if (recipeReviews != null && recipeReviews.Any())
            {
                List<RecipeFeedbackDto> recipeFeedbackDtos = recipeReviews
                    .Select(item => new RecipeFeedbackDto
                    {
                        Id = item.id,
                        Text = item.text,
                        Rate = item.Rate,
                        PostDate = item.PostDate,
                        RecipeId = item.RecipeId
                    })
                    .ToList();
                return Ok(recipeFeedbackDtos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var recipeFeedback = _recipeFeedbackRepository.GetById(id);
            if (recipeFeedback != null)
                return Ok(recipeFeedback);

            return NotFound();
        }

        [HttpPost]
        public ActionResult PostRecipeFeedback([FromBody] RecipeFeedbackDto recipeFeedbackDto)
        {
            if (recipeFeedbackDto == null)
            {
                return BadRequest("Invalid RecipeFeedback data.");
            }
            string userId = GetUserIdFromClaims(); // Get the user's ID from claims


            RecipeFeedback recipeFeedback = new RecipeFeedback
            {
                text = recipeFeedbackDto.Text,
                Rate = recipeFeedbackDto.Rate,
                PostDate = recipeFeedbackDto.PostDate,
                userId = iUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id,
                RecipeId = recipeFeedbackDto.RecipeId

            };

            _recipeFeedbackRepository.Add(recipeFeedback);
            int rowsAffected = _recipeFeedbackRepository.SaveChanges();
            if (rowsAffected > 0)
            {
                return Created("GetById", new { id = recipeFeedback.id });
            }

            return NotFound("RecipeFeedback creation failed.");
        }

        [HttpPut]
        public ActionResult UpdateRecipeFeedback([FromBody] RecipeFeedbackDto recipeFeedbackDto)
        {
            if (recipeFeedbackDto == null)
            {
                return BadRequest("Invalid RecipeFeedback data.");
            }
            RecipeFeedback recipeFeedback = _recipeFeedbackRepository.GetById(recipeFeedbackDto.Id);

            if (recipeFeedback == null)
                return NotFound("RecipeFeedback Not Found!");

            recipeFeedback.text = recipeFeedbackDto.Text;
            recipeFeedback.Rate = recipeFeedbackDto.Rate;
            recipeFeedback.PostDate = recipeFeedbackDto.PostDate;
            recipeFeedback.RecipeId = recipeFeedbackDto.RecipeId;

            _recipeFeedbackRepository.Update(recipeFeedback);
            int rowsAffected = _recipeFeedbackRepository.SaveChanges();
            if (rowsAffected > 0)
            {
                return NoContent();
            }

            return NotFound("RecipeFeedback update failed.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRecipeFeedback(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid RecipeFeedback id.");
            }

            var res = _recipeFeedbackRepository.GetById(id);
            if (res == null)
                return NotFound("RecipeFeedback Not Found!");

            _recipeFeedbackRepository.Delete(id);
            int rowsAffected = _recipeFeedbackRepository.SaveChanges();
            if (rowsAffected > 0)
            {
                return NoContent();
            }

            return NotFound("RecipeFeedback delete failed.");
        }

        [Authorize]
        [HttpGet("IsFeedbackAddedToRecipe/{recipeId}")]
        public ActionResult<bool> IsFeedbackAddedToRecipet(int recipeId)
        {
            int userId = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;
            RecipeFeedback recipeFeedback = _recipeFeedbackRepository.GetRecipeFeedbackByUserIdAndRecipeId(userId,recipeId);
            bool isFeedbackAddedToRecipe;
            if (recipeFeedback == null)
                isFeedbackAddedToRecipe = false;
            else
                isFeedbackAddedToRecipe = true;
            return isFeedbackAddedToRecipe;
        }
    }
}
