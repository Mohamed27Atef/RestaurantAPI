using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.Cart;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.CartRepository;
using RestaurantAPI.Repository.OrderRepository;

namespace RestaurantAPI.Controllers
{
    public class CartController : BaseApiClass
    {
        private readonly ICartRepository IcartRepo;
        public CartController(ICartRepository _IcartRepo)
        {
            this.IcartRepo = _IcartRepo;
        }
        //get

        [HttpGet()]
        public ActionResult getAll()
        {
            var allcarts = IcartRepo.GetAll();
            List<CartDTO> cartDtos = new List<CartDTO>();
            if (allcarts != null)
            {
                foreach (var item in allcarts)
                {
                    cartDtos.Add(new CartDTO()
                    {

                        Id = item.id                        
                    });
                }
                return Ok(cartDtos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            var cart = IcartRepo.GetById(id);
            if (cart != null)
                return Ok(cart);

            return NotFound();
        }


        [HttpPost]
        public ActionResult PostOrder([FromBody] CartDTO cartDto)
        {
            if (cartDto == null)
            {
                return BadRequest("Invalid Cart data.");
            }

            
            Cart cart = new Cart()
            {

                id = cartDto.Id
            };

            IcartRepo.Add(cart);
            int Raws = IcartRepo.SaveChanges();
            if (Raws > 0)
            {
                return CreatedAtAction("getById", new { id = cart.id }, cart);
            }


            return NotFound("Cart creation failed.");
        }

        [HttpPut]
        public ActionResult updateOrder(int id, [FromBody] CartDTO cartDto)
        {
            if (cartDto == null)
            {
                return BadRequest("Invalid Cart data.");
            }
            Cart cart = IcartRepo.GetById(id);

            if (cart == null)
                return NotFound("Cart Not Found!");


            //cart.userId = cartDto.UserId;

            IcartRepo.Update(cart);
            int Raws = IcartRepo.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Cart updated failed.");
        }

        [HttpDelete]
        public ActionResult deleteOrder(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid cart id.");
            }

            var res = IcartRepo.GetById(id);
            if (res == null)
                return NotFound("Cart Not Found!");

            IcartRepo.Delete(id);
            int Raws = IcartRepo.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Cart updated failed.");
        }


    }
}
