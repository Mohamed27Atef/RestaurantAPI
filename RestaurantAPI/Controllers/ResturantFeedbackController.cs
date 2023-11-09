using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.RecipeFeedBack;
using RestaurantAPI.Dto.ResturantFeedback;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.RecipeFeedBackRepository;
using RestaurantAPI.Repository.ResturantFeedBackRepository;

namespace RestaurantAPI.Controllers
{
    public class ResturantFeedbackController : BaseApiClass
    {
        private readonly IResturantFeedBackRepository iResturantFeedBackRepository;
        private readonly IUserRepository iUserRepository;

        public ResturantFeedbackController(IResturantFeedBackRepository iResturantFeedBackRepository, IUserRepository _IUserRepository)
        {
            this.iResturantFeedBackRepository = iResturantFeedBackRepository;
            iUserRepository = _IUserRepository;
        }


        //get
        [HttpGet("restaurant/{restaurantId}")]
        public ActionResult<IEnumerable<ResturantFeedbackDto>> GetReviewsForRestaurant(int restaurantId)
        {
            var restaurantReviews = iResturantFeedBackRepository.GetReviewsForRestaurant(restaurantId);

            if (restaurantReviews != null && restaurantReviews.Any())
            {
                List<ResturantFeedbackDto> resturantFeedbackDtos = restaurantReviews
                    .Select(item => new ResturantFeedbackDto
                    {
                        Id = item.id,
                        Text = item.text,
                        Rate = item.Rate,
                        PostDate = item.PostDate,
                        userName = item.User.ApplicationUser.UserName,
                        ResturantId = item.ResturantId,
                        //UserId = item.UserId,
                    })
                    .ToList();
                return Ok(resturantFeedbackDtos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            var resturantFeedback = iResturantFeedBackRepository.GetById(id);
            if (resturantFeedback != null)
                return Ok(resturantFeedback);

            return NotFound();
        }
        //post 

        [HttpPost]
        public ActionResult PostResturant([FromBody] ResturantFeedbackDto resturantFeedbackDto)
        {
            if (resturantFeedbackDto == null)
            {
                return BadRequest("Invalid ResturantFeedBack data.");
            }

            ResturantFeedback resturantFeedBack = new ResturantFeedback()
            {

                text = resturantFeedbackDto.Text,
                Rate = resturantFeedbackDto.Rate,
                PostDate = resturantFeedbackDto.PostDate,

                UserId = iUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id,

                ResturantId = resturantFeedbackDto.ResturantId
            };

            iResturantFeedBackRepository.Add(resturantFeedBack);
            int Raws = iResturantFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("ResturantFeedBack creation failed.");
        }


        [HttpPut]
        public ActionResult updateResturant([FromBody] ResturantFeedbackDto resturantFeedbackDto)
        {
            if (resturantFeedbackDto == null)
            {
                return BadRequest("Invalid ResturantFeedBack data.");
            }
            ResturantFeedback resturantFeedback = iResturantFeedBackRepository.GetById(resturantFeedbackDto.Id);

            if (resturantFeedback == null)
                return NotFound("ResturantFeedBack Not Found!");


            resturantFeedback.text = resturantFeedbackDto.Text;
            resturantFeedback.Rate = resturantFeedbackDto.Rate;
            resturantFeedback.PostDate = resturantFeedbackDto.PostDate;
            resturantFeedback.ResturantId = resturantFeedbackDto.ResturantId;
            ;

            iResturantFeedBackRepository.Update(resturantFeedback);
            int Raws = iResturantFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("ResturantFeedBack updated failed.");
        }

        [HttpDelete]
        public ActionResult deleteResturant(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ResturantFeedBack id.");
            }

            var res = iResturantFeedBackRepository.GetById(id);
            if (res == null)
                return NotFound("ResturantFeedBack Not Found!");

            iResturantFeedBackRepository.Delete(id);
            int Raws = iResturantFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }

            return NotFound("ResturantFeedBack updated failed.");
        }

        [Authorize]
        [HttpGet("IsFeedbackAddedToRestaurant/{RestaurantId}")]
        public ActionResult<bool> IsFeedbackAddedToRestaurant(int RestaurantId)
        {
            int userId = iUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;
            ResturantFeedback resturantFeedback = iResturantFeedBackRepository.GetRestaurantFeedbackByUserIdAndRestaurantId(userId, RestaurantId);
            bool isFeedbackAddedToRestaurant;
            if (resturantFeedback == null)
                isFeedbackAddedToRestaurant = false;
            else
                isFeedbackAddedToRestaurant = true;
            return isFeedbackAddedToRestaurant;
        }
    }
}
