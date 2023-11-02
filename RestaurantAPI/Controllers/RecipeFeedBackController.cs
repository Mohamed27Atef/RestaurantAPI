using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.RecipeFeedBack;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.RecipeFeedBackRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeFeedBackController : BaseApiClass
    {
 
        private readonly IRecipeFeedBackRepository iRecipeFeedBackRepository;

        public RecipeFeedBackController(IRecipeFeedBackRepository iRecipeFeedBackRepository)
        {
            this.iRecipeFeedBackRepository = iRecipeFeedBackRepository;
       
        }

        [HttpGet("getNumberOfReview/{recipeId}")]
        public ActionResult getNumberOfReview(int recipeId)
        {
            var r = iRecipeFeedBackRepository.getNumberOfRecipeReview(recipeId);
            return Ok(r);
        }

        //get

        [HttpGet()]
        public ActionResult getAll()
        {
            var allRecipeFeedBacks = iRecipeFeedBackRepository.GetAll();
            List<RecipeFeedbackDto> recipeFeedbackDtoDtos = new List<RecipeFeedbackDto>();
            if (allRecipeFeedBacks != null)
            {
                foreach (var item in allRecipeFeedBacks)
                {
                    recipeFeedbackDtoDtos.Add(new RecipeFeedbackDto()
                    {
                        Id = item.id,
                        Text = item.text,
                        Rate = item.Rate,
                        PostDate = item.PostDate,
                        UserId = item.userId,
                        RecipeId =item.userId
                    });
                }
                return Ok(recipeFeedbackDtoDtos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            var recipeFeedBack = iRecipeFeedBackRepository.GetById(id);
            if (recipeFeedBack != null)
                return Ok(recipeFeedBack);

            return NotFound();
        }

        //post 

        [HttpPost]
        public ActionResult PostResturant([FromBody] RecipeFeedbackDto recipeFeedbackDto)
        {
            if (recipeFeedbackDto == null)
            {
                return BadRequest("Invalid RecipeFeedBack data.");
            }

            RecipeFeedback recipeFeedback = new RecipeFeedback()
            {

                text = recipeFeedbackDto.Text,
                Rate = recipeFeedbackDto.Rate,
                PostDate = recipeFeedbackDto.PostDate,
                userId = recipeFeedbackDto.UserId,
                RecipeId = recipeFeedbackDto.UserId
            };

            iRecipeFeedBackRepository.Add(recipeFeedback);
            int Raws = iRecipeFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return CreatedAtAction("getById", new { id = recipeFeedback.id }, recipeFeedback);
            }


            return NotFound("RecipeFeedBack creation failed.");
        }


        [HttpPut]
        public ActionResult updateResturant([FromBody] RecipeFeedbackDto recipeFeedbackDto)
        {
            if (recipeFeedbackDto == null)
            {
                return BadRequest("Invalid RecipeFeedback data.");
            }
            RecipeFeedback recipeFeedback = iRecipeFeedBackRepository.GetById(recipeFeedbackDto.Id);

            if (recipeFeedback == null)
                return NotFound("RecipeFeedback Not Found!");


            recipeFeedback.text = recipeFeedbackDto.Text;
            recipeFeedback.Rate = recipeFeedbackDto.Rate;
            recipeFeedback.PostDate = recipeFeedbackDto.PostDate;
            recipeFeedback.userId = recipeFeedbackDto.UserId;
            recipeFeedback.RecipeId = recipeFeedbackDto.RecipeId;
       ;

            iRecipeFeedBackRepository.Update(recipeFeedback);
            int Raws = iRecipeFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }

        
            return NotFound("RecipeFeedback updated failed.");
        }

        [HttpDelete]
        public ActionResult deleteResturant(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid RecipeFeedback id.");
            }

            var res = iRecipeFeedBackRepository.GetById(id);
            if (res == null)
                return NotFound("RecipeFeedback Not Found!");

            iRecipeFeedBackRepository.Delete(id);
            int Raws = iRecipeFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("RecipeFeedback updated failed.");
        }

    }
}
