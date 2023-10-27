using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.OrderRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IOrderRepository IorderRepo;
        public orderController(IOrderRepository _IorderRepo)
        {
            this.IorderRepo = _IorderRepo;
        }
        //get

        [HttpGet()]
        public ActionResult getAll()
        {
            var allOrders = IorderRepo.getAll();
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
        public ActionResult getById(int id)
        {
            var order = IorderRepo.getById(id);
            if (order != null)
                return Ok(order);

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

            IorderRepo.add(order);
            int Raws = IorderRepo.SaveChanges();
            if (Raws > 0)
            {
                return CreatedAtAction("getById", new { id = order.Id }, order);
            }


            return NotFound("Order creation failed.");
        }

        [HttpPut]
        public ActionResult updateOrder(int id,[FromBody] OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Invalid Order data.");
            }
            Order order = IorderRepo.getById(id);

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




            IorderRepo.update(order);
            int Raws = IorderRepo.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Order updated failed.");
        }

        [HttpDelete]
        public ActionResult deleteOrder(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid order id.");
            }

            var res = IorderRepo.getById(id);
            if (res == null)
                return NotFound("Order Not Found!");

            IorderRepo.delete(id);
            int Raws = IorderRepo.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("Order updated failed.");
        }

        
    }
}
