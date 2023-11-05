using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.RecipeFeedBack;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.RecipeFeedBackRepository;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeFeedbackController : ControllerBase
    {
        private readonly IRecipeFeedBackRepository _recipeFeedbackRepository;

        public RecipeFeedbackController(IRecipeFeedBackRepository recipeFeedbackRepository)
        {
            _recipeFeedbackRepository = recipeFeedbackRepository;
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
                        UserId = item.userId,
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

            RecipeFeedback recipeFeedback = new RecipeFeedback
            {
                text = recipeFeedbackDto.Text,
                Rate = recipeFeedbackDto.Rate,
                PostDate = recipeFeedbackDto.PostDate,
                RecipeId = recipeFeedbackDto.RecipeId
            };

            _recipeFeedbackRepository.Add(recipeFeedback);
            int rowsAffected = _recipeFeedbackRepository.SaveChanges();
            if (rowsAffected > 0)
            {
                return CreatedAtAction("GetById", new { id = recipeFeedback.id }, recipeFeedback);
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
    }
}
