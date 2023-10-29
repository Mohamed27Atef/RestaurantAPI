using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.ProductRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var allREsturants = resturantRepository.GetAll();
            List<ResturantDto> resturantDtos = new List<ResturantDto>();
            if(allREsturants != null)
            {
                foreach (var item in allREsturants)
                {
                    resturantDtos.Add(new ResturantDto()
                    {
                        Address = item.Address,
                        Cusinetype = item.Cusinetype,
                        id = item.id,
                        Image = item.Image,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        Name = item.Name,
                        OpenHours = item.OpenHours,
                        Rate = item.Rate,
                    });
                }
                return Ok(resturantDtos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            var resturant = resturantRepository.GetById(id);


            if (resturant != null)
            {
                GetOneRestaurantDto restaurantDto = new GetOneRestaurantDto()
                {
                    Address = resturant.Address,
                    closingHours = resturant.ClosingHours,
                    Cusinetype = resturant.Cusinetype,
                    id = resturant.id,
                    Image = resturant.Image,
                    Latitude = resturant.Latitude,
                    Longitude = resturant.Longitude,
                    Name = resturant.Name,
                    OpenHours = resturant.OpenHours,
                    Rate = resturant.Rate,
                    email = resturant.email,
                    phone = resturant.phone
                };

                foreach (var item in resturant.RestaurantImages)
                    restaurantDto.images.Add(item.imageUrl);
            
                foreach (var item in resturant.ClosingDays)
                    restaurantDto.clossingDays.Add(item.day.ToString());
            

                return Ok(restaurantDto);
            }

            return NotFound();
        }

        [HttpGet("getimages/{id}")]
        public ActionResult getRestaurantImages(int id)
        {
            var resturant = resturantRepository.GetById(id);


            if (resturant != null)
                return Ok(resturantRepository.getResaurantIamges(id));

            return NotFound();
        }



        [HttpGet("search")]
        public ActionResult getByNameAndCategory(string q, int cat)
        {
            var resturant = resturantRepository.getByNameAndCategoryId(q, cat);
            if (resturant != null)
                return Ok(resturant);
            return NotFound();
        }

        [HttpGet("search/{q}")]
        public ActionResult getByName(string q)
        {
            var resturant = resturantRepository.getByName(q);
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

        [HttpGet("getByCategory/{category_id}")]
        public ActionResult getByCategory(int category_id)
        {
            var resturant = resturantRepository.getByCategoryId(category_id);

            if (resturant != null)
                return Ok(resturant);

            return NotFound();
        }


        //post 

        [HttpPost]
        public ActionResult PostResturant([FromBody] ResturantDto resturantDto)
        {
            if (resturantDto == null)
            {
                return BadRequest("Invalid resturant data.");
            }

            Resturant resturant = new Resturant()
            {
                id = resturantDto.id,
                Name = resturantDto.Name,
                Address = resturantDto.Address,
                Cusinetype = resturantDto.Cusinetype,
                Longitude = resturantDto.Longitude,
                Latitude = resturantDto.Latitude,
                Rate = resturantDto.Rate,
                OpenHours = resturantDto.OpenHours,
                Image = resturantDto.Image,

            };
            
            resturantRepository.Add(resturant);
            int Raws = resturantRepository.SaveChanges();
            if (Raws > 0)
            {
                return CreatedAtAction("getById", new { id = resturant.id }, resturant);
            }
                

            return NotFound("Restaurant creation failed.");
        }

        [HttpPut]
        public ActionResult updateResturant([FromBody] ResturantDto resturantDto)
        {
            if (resturantDto == null)
            {
                return BadRequest("Invalid resturant data.");
            }
            Resturant resturant = resturantRepository.GetById(resturantDto.id);

            if (resturant == null)
                return NotFound("Resturant Not Found!");

            resturant.id = resturantDto.id;
            resturant.Name = resturantDto.Name;
            resturant.Address = resturantDto.Address;
            resturant.Cusinetype = resturantDto.Cusinetype;
            resturant.Longitude = resturantDto.Longitude;
            resturant.Latitude = resturantDto.Latitude;
            resturant.Rate = resturantDto.Rate;
            resturant.OpenHours = resturantDto.OpenHours;
            resturant.Image = resturantDto.Image;
           

            resturantRepository.Update(resturant);
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

            var res = resturantRepository.GetById(id);
            if (res == null)
                return NotFound("Resturant Not Found!");

            resturantRepository.Delete(id);
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
            var res = resturantRepository.GetById(id);
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
            resturantRepository.Update(res);
            int rowsAffected = resturantRepository.SaveChanges();

            if (rowsAffected > 0)
            {
                return Ok("Image updated successfully.");
            }

            return NotFound("Image update failed.");
        }
    }
}
