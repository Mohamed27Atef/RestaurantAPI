using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Table;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.LocationRepository;
using RestaurantAPI.Repository.ProductRepository;
using RestaurantAPI.Repository.RestaurantCateigoryRespository;
using RestaurantAPI.Repository.RestaurantImageRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;
using System.Net.Http;
using System.Text;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{

    public class ResturantController : BaseApiClass
    {

        private readonly IResturanrRepo resturantRepository;
        private readonly ImageService imageService;
        private readonly IRestaurantImageRepository iRestaurantImageRepository;
        private readonly IRestaurantCateigoryRepository iRestaurantCateigoryRepository;
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly SignInManager<ApplicationIdentityUser> signInManager;
        private readonly IUserRepository userRepository;
        public ResturantController(IResturanrRepo resturantRepository,
            ImageService imageService, IRestaurantCateigoryRepository iRestaurantCateigoryRepository,
            IRestaurantImageRepository iRestaurantImageRepository,
            UserManager<ApplicationIdentityUser> userManager, SignInManager<ApplicationIdentityUser> signInManager,IUserRepository userRepository)
        {
            this.resturantRepository = resturantRepository;
            this.imageService = imageService;
            this.iRestaurantCateigoryRepository = iRestaurantCateigoryRepository;
            this.iRestaurantImageRepository = iRestaurantImageRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;

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
                    id = resturant.id,
                    Address = resturant.Address,
                    closingHours = resturant.ClosingHours,
                    Cusinetype = resturant.Cusinetype,
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

        [HttpGet("getByAddress/{address}")]
        public ActionResult getRestaurantByLocation(string address)
        {
            return Ok(resturantRepository.getByAddress(address));
        }

        [HttpGet("search")]
        public ActionResult getByNameAndCategory(string q, int cat)
        {
            var resturant = resturantRepository.getByNameAndCategoryId(q, cat);
            if (resturant != null)
                return Ok(resturant);
            return NotFound();
        }

        [HttpGet("searchByLocatoinAndCategoryAndName")]
        public ActionResult searchByLocatoinAndCategoryAndName(string q, int cat, string location)
        {
            var resturant = resturantRepository.getByLocatoinAndCagegoryAndName(q, cat, location);
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

        [HttpGet("getTableRestaurant/{restaurantId}")]
        public ActionResult getTableOfRestaurant(int restaurantId)
        {
            var tables = resturantRepository.getTaleRestaurant(restaurantId);
            List< TablerestaurantDto> tablerestaurantDto = new();
            foreach (var item in tables)
            {
                tablerestaurantDto.Add(new TablerestaurantDto()
                {
                    id = item.Id,
                    tableType = item.TableType.ToString()
                });
            }

            return Ok(tablerestaurantDto);
        }

        [HttpGet("getOpenCloseHours/{restaurantId}")]
        public ActionResult getOpenClosingHours(int restaurantId)
        {
            return Ok(resturantRepository.getOpenCloseHours(restaurantId)); 
        }


        //post 
        //[HttpPost()]
        //public ActionResult PostResturant()
        //{
        //    return Ok();
        //}
        [HttpPost()]
        public async Task<IActionResult> PostResturant([FromBody] ResturantDto resturantDto)
        {
            if (resturantDto is null)
            {
                return BadRequest("Invalid resturant data.");
            }
            Resturant res = resturantRepository.GetById(resturantDto.id);

            if (res != null)
            {
                ActionResult res1 =updateResturant(resturantDto);
                return res1;
            }
            #region createUser
            RegisterDto userDto = new RegisterDto
            {
                FirstName = resturantDto.email.Split('@')[0],
                LastName = resturantDto.email.Split('@')[0],
                Email = resturantDto.email,
                Password = resturantDto.Password,
                

            };
            bool emailExists = await CheckIfEmailExists(userDto.Email);
            if (emailExists)
                return BadRequest("Email already exists");

            var user = new ApplicationIdentityUser
            {

                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.Email,
                Email = userDto.Email,
                CreatedAt = DateTime.UtcNow,
                Address = resturantDto.Address
            };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                // Optionally sign in the user after registration.
                await signInManager.SignInAsync(user, isPersistent: false);
                var resutlRole = await userManager.AddToRoleAsync(user, "admin");
                User myUser = new User()
                {
                    application_user_id = user.Id,
                };
                userRepository.Add(myUser);
                userRepository.SaveChanges();
            }

            #endregion

            Resturant resturant = new Resturant()
            {
                email = resturantDto.email,
                Password = resturantDto.Password,
                Name = resturantDto.Name,
                Address = resturantDto.Address,
                Cusinetype = resturantDto.Cusinetype,
                Longitude = resturantDto.Longitude,
                Latitude = resturantDto.Latitude,
                OpenHours = resturantDto.OpenHours,
                ClosingHours = resturantDto.ClosingHours,
                Image = resturantDto.Image,
                Description = resturantDto.Description,
                phone = resturantDto.phone,
                ApplicationIdentityUserID = user.Id

            };
          
            resturantRepository.Add(resturant);
            int Raws = resturantRepository.SaveChanges();
            if (Raws > 0)
            {

                foreach (var categoryId in resturantDto.RestaurantCategories)
                {
                    var resCategory = iRestaurantCateigoryRepository.GetByIdAndResutrantId(categoryId, resturant.id);
                    if (resCategory is null)
                    {
                        RestaurantCateigory resCat = new RestaurantCateigory { RestaurantId = resturant.id, CategoryId = categoryId };
                        this.iRestaurantCateigoryRepository.Add(resCat);
                    }
                }
                this.iRestaurantCateigoryRepository.SaveChanges();

                foreach (var imagUrl in resturantDto.images)
                {

                    var imgResturantFound =this.iRestaurantImageRepository.GetByIdAndImg(resturant.id, imagUrl);
                    if(imgResturantFound is null)
                    {
                        RestaurantImage resImg = new RestaurantImage { restaurantId = resturant.id, imageUrl = imagUrl };
                        this.iRestaurantImageRepository.Add(resImg);
                        this.iRestaurantImageRepository.SaveChanges();

                    }

                }

                return NoContent();
            }


            return NotFound("Restaurant creation failed.");
        }
     
        private async Task<bool> CheckIfEmailExists(string email)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            return (existingUser != null);
        }


        [HttpGet("getByAppID")]
        [Authorize]
        public ActionResult getByApplicationId()
        {
            string AppId = GetUserIdFromClaims();
            Resturant resturant = resturantRepository.getByAppId(AppId);

            if(resturant is null)
            {
                return NotFound("Resturant Not Found");
            }

            return Ok(resturant);
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
            resturant.phone = resturantDto.phone;
            resturant.Image = resturantDto.Image;
           

            resturantRepository.Update(resturant);
            int Raws = resturantRepository.SaveChanges();
            if (Raws > 0)
            {
                var allRestaurantImage = iRestaurantImageRepository.GetAllById(resturant.id);
              
                foreach (var item in allRestaurantImage)
                    this.iRestaurantImageRepository.Delete(resturant.id);
                this.iRestaurantImageRepository.SaveChanges();

                foreach (var imagUrl in resturantDto.images)
                {
                    var imgResturantFound = this.iRestaurantImageRepository.GetByIdAndImg(resturant.id, imagUrl);
                    if (imgResturantFound is null)
                    {
                        RestaurantImage resImg = new RestaurantImage { restaurantId = resturant.id, imageUrl = imagUrl };
                        this.iRestaurantImageRepository.Add(resImg);
                        this.iRestaurantImageRepository.SaveChanges();
                    }
                }
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
