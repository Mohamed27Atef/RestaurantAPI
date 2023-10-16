using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.ProductRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{

    public class ResturantController : BaseApiClass
    {

        private readonly IResturanrRepo resturantRepository;
        private readonly ImageService imageService;

        public ResturantController(IResturanrRepo resturantRepository, ImageService imageService)
        {
            this.resturantRepository = resturantRepository;
            this.imageService = imageService;
        }
        //get

        [HttpGet()]
        public ActionResult getAll()
        {
            var allREsturants = resturantRepository.getAll();
            if(allREsturants != null)
                return Ok(allREsturants);

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            var resturant = resturantRepository.getById(id);
            if (resturant != null)
                return Ok(resturant);

            return NotFound();
        }

        [HttpGet("/getByName/{name}")]
        public ActionResult getByName(string name)
        {
            var resturant = resturantRepository.getByName(name);
            if (resturant != null)
                return Ok(resturant);

            return NotFound();
        }

        [HttpGet("getByAddress")]
        public ActionResult getByAddress(string address)
        {
            var resturant = resturantRepository.getByAddress(address);
            if (resturant != null)
                return Ok(resturant);

            return NotFound();
        }

        //post 

        [HttpPost]
        public ActionResult PostResturant([FromBody] Resturant resturant)
        {
            if (resturant == null)
            {
                return BadRequest("Invalid resturant data.");
            }


            resturantRepository.add(resturant);
            int Raws = resturantRepository.SaveChanges();
            if (Raws > 0)
            {
                return CreatedAtAction("getById", new { id = resturant.id }, resturant);
            }
                

            return NotFound("Restaurant creation failed.");
        }

        [HttpPut]
        public ActionResult updateResturant([FromBody] Resturant resturant)
        {
            if (resturant == null)
            {
                return BadRequest("Invalid resturant data.");
            }

            var res = resturantRepository.getById(resturant.id);
            if (res == null)
                return NotFound("Resturant Not Found!");

            resturantRepository.update(resturant);
            int Raws = resturantRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Restaurant updated failed.");
        }

        [HttpDelete]
        public ActionResult deleteResturant(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid resturant id.");
            }

            var res = resturantRepository.getById(id);
            if (res == null)
                return NotFound("Resturant Not Found!");

            resturantRepository.delete(id);
            int Raws = resturantRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Restaurant updated failed.");
        }

        [HttpPut("updateImage/{id}")]
        public ActionResult UpdateRestaurantImage(int id, IFormFile newImage)
        {
            var res = resturantRepository.getById(id);
            if (res == null)
                return NotFound("Resturant Not Found!");

            if (newImage == null || newImage.Length == 0)
            {
                return BadRequest("Invalid image file.");
            }

            string imageUrl = imageService.SaveImage(newImage);

            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("Failed to save the image.");
            }

            res.Image = imageUrl;
            resturantRepository.update(res);
            int rowsAffected = resturantRepository.SaveChanges();

            if (rowsAffected > 0)
            {
                return Ok("Image updated successfully.");
            }

            return NotFound("Image update failed.");
        }
    }
}
