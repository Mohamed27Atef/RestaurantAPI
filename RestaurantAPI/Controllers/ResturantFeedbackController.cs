using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.RecipeFeedBack;
using RestaurantAPI.Dto.ResturantFeedback;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.RecipeFeedBackRepository;
using RestaurantAPI.Repository.ResturantFeedBackRepository;

namespace RestaurantAPI.Controllers
{
    public class ResturantFeedbackController : BaseApiClass
    {
        private readonly IResturantFeedBackRepository iResturantFeedBackRepository;

        public ResturantFeedbackController(IResturantFeedBackRepository iResturantFeedBackRepository)
        {
            this.iResturantFeedBackRepository = iResturantFeedBackRepository;

        }


        //get
        [HttpGet]
        public ActionResult<IEnumerable<ResturantFeedbackDto>> GetAll()
        {
            var alliResturantFeedBacks = iResturantFeedBackRepository.GetAll();

            if (alliResturantFeedBacks != null)
            {
                List<ResturantFeedbackDto> resturantFeedbackDtos = alliResturantFeedBacks
                    .Select(item => new ResturantFeedbackDto
                    {
                        Id = item.id,
                        Text = item.text,
                        Rate = item.Rate,
                        PostDate = item.PostDate,
                        ResturantId = item.ResturantId,
                        UserId = item.UserId,
                    })
                    .ToList();
                return Ok(resturantFeedbackDtos);
            }

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
                UserId = 1,
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
            resturantFeedback.UserId = resturantFeedbackDto.UserId;
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
    }
}
