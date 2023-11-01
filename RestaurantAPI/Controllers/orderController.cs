using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
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


        public OrderController(IOrderRepository _IorderRepo, ICartRepository _ICartRepository,
            ICartUserRrepository _ICartUserRrepository, IUserRepository _IUserRepository)
        {
            this.IorderRepo = _IorderRepo;
            this.ICartRepositoryo = _ICartRepository;
            this.ICartUserRepository = _ICartUserRrepository;
            this.IUserRepository = _IUserRepository;

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
                        OrderDate = item.OrderDate,
                        UpdatedAt = item.UpdatedAt,
                        TotalPrice = item.TotalPrice,
    
                        Location = item.Location,
                        DeliveryTime = item.DeliveryTime,
                        UserId = item.UserId,
                        DeliveryId = item.DeliveryId,
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
        public ActionResult GetByLocation([FromQuery] string location)
        {
            var orders = IorderRepo.GetByLocation(location);
            if (orders != null)
                return Ok(orders);

            return NotFound();
        }
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
        public ActionResult PostOrder([FromBody] OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Invalid Order data.");
            }

            Order order = new Order()
            {
               CreatedAt = orderDto.CreatedAt,
               OrderDate = orderDto.OrderDate,
               UpdatedAt = orderDto.UpdatedAt,
               TotalPrice = orderDto.TotalPrice,
               Location  = orderDto.Location,
               DeliveryTime  = orderDto.DeliveryTime,
               UserId  = orderDto.UserId,
               DeliveryId = orderDto.DeliveryId,
               CartId  = orderDto.CartId,
               AddressId = orderDto.AddressId

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
            order.OrderDate = orderDto.OrderDate;
            order.UpdatedAt = orderDto.UpdatedAt;
            order.TotalPrice = orderDto.TotalPrice;
     
            order.Location = orderDto.Location;
            order.DeliveryTime = orderDto.DeliveryTime;
            order.UserId = orderDto.UserId;
            order.DeliveryId = orderDto.DeliveryId;
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
