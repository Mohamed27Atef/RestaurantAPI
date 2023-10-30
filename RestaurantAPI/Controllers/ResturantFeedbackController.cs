using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.RecipeFeedBack;
using RestaurantAPI.Dto.ResturantFeedback;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.RecipeFeedBackRepository;
using RestaurantAPI.Repository.ResturantFeedBackRepository;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantFeedbackController : BaseApiClass
    {
        private readonly IResturantFeedBackRepository iResturantFeedBackRepository;

        public ResturantFeedbackController(IResturantFeedBackRepository iResturantFeedBackRepository)
        {
            this.iResturantFeedBackRepository = iResturantFeedBackRepository;

        }


        //get

        [HttpGet()]
        public ActionResult getAll()
        {
            var alliResturantFeedBacks = iResturantFeedBackRepository.GetAll();
            List<ResturantFeedbackDto> resturantFeedbackDtos = new List<ResturantFeedbackDto>();
            if (alliResturantFeedBacks != null)
            {
                foreach (var item in resturantFeedbackDtos)
                {
                    resturantFeedbackDtos.Add(new ResturantFeedbackDto()
                    {
                        Id = item.Id,
                        Text = item.Text,
                        Rate = item.Rate,
                        PostDate = item.PostDate,
                        ResturantId = item.ResturantId,
                        UserId = item.UserId,
                    });
                }
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
                UserId = resturantFeedbackDto.UserId,
                ResturantId = resturantFeedbackDto.ResturantId
            };

            iResturantFeedBackRepository.Add(resturantFeedBack);
            int Raws = iResturantFeedBackRepository.SaveChanges();
            if (Raws > 0)
            {
                return CreatedAtAction("getById", new { id = resturantFeedBack.id }, resturantFeedBack);
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
