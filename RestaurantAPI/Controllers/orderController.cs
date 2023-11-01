using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Address;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.AddressRepository;
using RestaurantAPI.Repository.CartRepository;
using RestaurantAPI.Repository.OrderRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiClass
    {
        private readonly IOrderRepository IorderRepo;
        private readonly ICartRepository ICartRepositoryo;
        private readonly ICartUserRrepository ICartUserRepository;
        private readonly IUserRepository IUserRepository;
        private readonly IAddressRepository IAddressRepository;
        public OrderController(IOrderRepository _IorderRepo, ICartRepository _ICartRepository,
            ICartUserRrepository _ICartUserRrepository, IUserRepository _IUserRepository, IAddressRepository IAddressRepository)
        {
            this.IorderRepo = _IorderRepo;
            this.ICartRepositoryo = _ICartRepository;
            this.ICartUserRepository = _ICartUserRrepository;
            this.IUserRepository = _IUserRepository;
            this.IAddressRepository = IAddressRepository;

        }
        //get

        [HttpGet()]
        public ActionResult GetAll()
        {
            var allOrders = IorderRepo.GetAll();
            List<OrderDTO> orderDtos = new List<OrderDTO>();
            if (allOrders != null)
            {
                foreach (var item in allOrders)
                {
                    orderDtos.Add(new OrderDTO()
                    {
                  

                        CreatedAt = item.CreatedAt,
                      
                        TotalPrice = item.TotalPrice,
    
                        
                  
                        UserId = item.UserId,
                      
                        CartId = item.CartId,
                        AddressId = item.AddressId
                    });
                }
                return Ok(orderDtos);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var order = IorderRepo.GetById(id);
            if (order != null)
                return Ok(order);

            return NotFound();
        }
        [HttpGet("byLocation")]
        
        [HttpGet("OrderTotalPrice/{id}")]
        public ActionResult GetOrderTotalPrice( int id)
        {
            var orderTotalPrice  = IorderRepo.GetOrderByIdTotalPrice(id);
            if (orderTotalPrice != null)
                return Ok(orderTotalPrice);

            return NotFound();
        }

        [HttpGet("allOrdersTotalPrice/")]
        public ActionResult GetAllOrdersTotalPrice()
        {
            var allOrdersTotalPrice = IorderRepo.GetAllOrderTotalPrice();
            if (allOrdersTotalPrice != null)
                return Ok(allOrdersTotalPrice);

            return NotFound();
        }
        //post 

        [HttpPost]
        public ActionResult PostOrder([FromBody] OrderAddressDTO orderAddressDTO)
        {
            if (orderAddressDTO == null)
            {
                return BadRequest("Invalid Order data.");
            }
            Cart newOrderCart = ICartUserRepository.getCartByUserId(IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
            AddressDTO addressDto = new AddressDTO { Street = orderAddressDTO.Street, City = orderAddressDTO.City, Country = orderAddressDTO.Country };
           int addressid = MakeAddressOrderd(addressDto); 
            Order order = new Order()
            {
               CreatedAt = DateTime.Now,
               Status = OrderStatus.Pending,
               TotalPrice = orderAddressDTO.TotalPrice,
               UserId  = IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id,
               CartId  = newOrderCart.id,
               AddressId = addressid

            };

            IorderRepo.Add(order);
            int Raws = IorderRepo.SaveChanges();
            if (Raws > 0)
            {
                MakeCartOrderd(order);
              
                return CreatedAtAction("getById", new { id = order.Id }, order);
            }

            
            return NotFound("Order creation failed.");
        }
        private int MakeAddressOrderd(AddressDTO addressDto)
        {
            Address address = new Address
            {
                Street = addressDto.Street,
                City = addressDto.City,
                Country = addressDto.Country

            };
            IAddressRepository.Add(address);
            IAddressRepository.SaveChanges();

            return address.Id;
        }
            private void MakeCartOrderd(Order order)
        {
            Cart newOrderCart = ICartUserRepository.getCartByUserId(IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
            newOrderCart.OrderId  = order.Id;
            ICartRepositoryo.Update(newOrderCart);
            int r = ICartRepositoryo.SaveChanges();

            //User user = IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims());
            //CartUser cartUserOrderd = new CartUser { cart_id = newOrderCart.id, user_id = user.id };
            //ICartUserRepository.Add(cartUserOrderd);
            //ICartUserRepository.SaveChanges();

        }
        [HttpPut]
        public ActionResult UpdateOrder(int id,[FromBody] OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Invalid Order data.");
            }
            Order order = IorderRepo.GetById(id);

            if (order == null)
                return NotFound("Order Not Found!");


            order.CreatedAt = orderDto.CreatedAt;
            order.TotalPrice = orderDto.TotalPrice;
            order.UserId = orderDto.UserId;
            order.CartId = orderDto.CartId;
            order.AddressId = orderDto.AddressId;




            IorderRepo.Update(order);
            int Raws = IorderRepo.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Order updated failed.");
        }

        [HttpDelete]
        public ActionResult DeleteOrder(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid order id.");
            }

            var res = IorderRepo.GetById(id);
            if (res == null)
                return NotFound("Order Not Found!");

            IorderRepo.Delete(id);
            int Raws = IorderRepo.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Order updated failed.");
        }

        
    }
}
