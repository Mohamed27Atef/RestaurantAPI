using Microsoft.AspNetCore.Authorization;
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
using RestaurantAPI.Repository.RestaurantOrderStatus;
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
        private readonly IUserRepository IUserRepository;
        private readonly IAddressRepository IAddressRepository;
        private readonly IRestaurantOrderStatus restaurantOrderStatus;
        private readonly IResturanrRepo resturanrRepo;
        private readonly ICartItemRepository cartItemRepository;

        public OrderController(IOrderRepository _IorderRepo, ICartRepository _ICartRepository,
             IUserRepository _IUserRepository, IAddressRepository IAddressRepository,
             IRestaurantOrderStatus restaurantOrderStatus,
             IResturanrRepo resturanrRepo,
             ICartItemRepository cartItemRepository)
        {
            this.IorderRepo = _IorderRepo;
            this.ICartRepositoryo = _ICartRepository;
            this.IUserRepository = _IUserRepository;
            this.IAddressRepository = IAddressRepository;
            this.restaurantOrderStatus = restaurantOrderStatus;
            this.resturanrRepo = resturanrRepo;
            this.cartItemRepository = cartItemRepository;
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
                        AddressId = item.AddressId
                    });
                }
                return Ok(orderDtos);
            }

            return NotFound();
        }
        [HttpGet("getAllOrderOfUser")]
        [Authorize]
        public ActionResult getAllOrderOfUser()
        {
            int userId = IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;
            var userOrdersRestaurant = IorderRepo.getOrderOfUsresByRestaurant(userId, cartItemRepository);

            
            return Ok(userOrdersRestaurant);
        }

        [HttpGet("getOrderByReataurantId")]
        [Authorize]
        public ActionResult getOrderByReataurantId()
        {
            string AppId = GetUserIdFromClaims();
            Resturant resturant = resturanrRepo.getByAppId(AppId);

            List<OrderAdmin> orders = IorderRepo.getOrderByReataurantId(resturant.id).Where(r => r != null).DistinctBy(r => r.Id).Select(r => new OrderAdmin()
            {
                customerName = r.User.ApplicationUser.UserName,
                customerPhone = r.User.ApplicationUser.PhoneNumber,
                date = r.CreatedAt,
                country = r.Address.Country,
                street = r.Address.Street,
                city = r.Address.City,
                orderId = r.Id,
                totalPrice = r.TotalPrice

            }).ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                orders[i].totalPrice = getTotalPriceOrderByRestaurantIdAndOrderId(orders[i].orderId, resturant.id);
                orders[i].status = restaurantOrderStatus.getStatusByRestaurntIdCartId(resturant.id, IorderRepo.getCartIdByOrderId(orders[i].orderId)).ToString();

            }

            return Ok(orders);
        }

        [HttpPut("updateStatus/{orderId}/{status}")]
        [Authorize]
        public ActionResult updateStatus(int orderId,  string status)
        {
            string AppId = GetUserIdFromClaims();
            Resturant resturant = resturanrRepo.getByAppId(AppId);
            Order order = IorderRepo.GetById(orderId);
            if (order == null)
                return BadRequest();

            var t = IorderRepo.getCartIdByOrderId(orderId);


            restaurantOrderStatus.updateStatus(resturant.id, IorderRepo.getCartIdByOrderId(orderId), (OrderStatus)IorderRepo.getStatusId(status));
            restaurantOrderStatus.SaveChanges();
            
            order.Status = (OrderStatus)IorderRepo.getStatusId(status);
            IorderRepo.Update(order);
            IorderRepo.SaveChanges();
            return Ok("done");
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
            if (orderAddressDTO is null || orderAddressDTO.Street == "" || orderAddressDTO.City == ""|| orderAddressDTO.Country == "")
            {
                return BadRequest("Invalid Order data.");
            }
            Cart newOrderCart = ICartRepositoryo.getCartByUserId(IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
            AddressDTO addressDto = new AddressDTO { Street = orderAddressDTO.Street, City = orderAddressDTO.City, Country = orderAddressDTO.Country };
           int addressid = MakeAddressOrderd(addressDto);
            Order order = new Order()
            {
               CreatedAt = DateTime.Now,
               Status = OrderStatus.shipped,
               TotalPrice = orderAddressDTO.TotalPrice,
               UserId  = IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id,
               AddressId = addressid

            };

            IorderRepo.Add(order);
            int Raws = IorderRepo.SaveChanges();
            List<int> restaurantIds = IorderRepo.createRestaruantOrderStatus(newOrderCart.id);
            foreach (var item in restaurantIds)
            {
                restaurantOrderStatus.Add(new RestaruantOrdersStatus()
                {
                    cartId = newOrderCart.id,
                    restaurantId = item,
                });
            }
            restaurantOrderStatus.SaveChanges();
            if (Raws > 0)
            {
                MakeCartOrderd(order);
              
                return Created("getById", new { id = order.Id });
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
            Cart newOrderCart = ICartRepositoryo.getCartByUserId(IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
            newOrderCart.OrderId  = order.Id;
            ICartRepositoryo.Update(newOrderCart);
            int r = ICartRepositoryo.SaveChanges();

            //User user = IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims());
            //CartUser cartUserOrderd = new CartUser { cart_id = newOrderCart.id, user_id = user.id };
            //ICartUserRepository.Add(cartUserOrderd);
            //ICartUserRepository.SaveChanges();

        }
        [HttpPut]
        [Authorize(Roles = "admin")]
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

        internal decimal getTotalPriceOrderByRestaurantIdAndOrderId(int orderId, int restaurantId)
        {
            Cart cart = ICartRepositoryo.getCatByOrderId(orderId);
            return cartItemRepository.getTotalPriceOrderByRestaurantIdAndOrderId(cart.id, restaurantId);

        }


    }
}
