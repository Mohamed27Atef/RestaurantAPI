using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.Cart;
using RestaurantAPI.Dto.CartItem;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.CartRepository;
using RestaurantAPI.Repository.ResturantRepository;

namespace RestaurantAPI.Controllers
{
    public class CartItemController : BaseApiClass
    {
        private readonly IUserRepository userRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly ICartRepository IcartRepo;
        private readonly IResturanrRepo resturantRepository;

        public CartItemController( 
            IUserRepository userRepository,
            ICartItemRepository cartItemRepository,
            ICartRepository IcartRepo,
            IResturanrRepo resturantRepository
           )
        {
            this.userRepository = userRepository;
            this.cartItemRepository = cartItemRepository;
            this.IcartRepo = IcartRepo;
            this.resturantRepository = resturantRepository;
        }
        [HttpGet]
        [Authorize]
        public ActionResult getCartItems()
        {
            Cart cart = IcartRepo.getCartByUserId(userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
            List<CartItem> items = cartItemRepository.GetAllByCartId(cart.id);
            List<CartItemDto> cartItemDto = new List<CartItemDto>();

            foreach (var item in items)
            {
                if (item.Cart.OrderId == null)
                {
                    cartItemDto.Add(new CartItemDto()
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        recipeName = item.Recipe.name,
                        restaurantName = item.Resturant.Name,
                        TotalPrice = item.TotalPrice,
                        recipePrice  =item.Recipe.Price,
                        imageUrl = item.Recipe.imageUrl,
                        recipeDescription = item.Recipe.Description,
                        recipeId = item.RecipeId,
                    });
                }
            }
                

            return Ok(cartItemDto);
        }

        [HttpGet("getCartItemsOrderd")]
        [Authorize]
        public ActionResult getCartItemsOrderd()
        {
            Cart cart = IcartRepo.getCartByUserId(userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
            List<CartItem> items = cartItemRepository.GetAllByCartId(cart.id);
            List<CartItemDto> cartItemDto = new List<CartItemDto>();

            foreach (var item in items)
            {
                if (item.Cart.OrderId != null)
                {
                    cartItemDto.Add(new CartItemDto()
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        recipeName = item.Recipe.name,
                        restaurantName = item.Resturant.Name,
                        TotalPrice = item.TotalPrice,
                        recipePrice = item.Recipe.Price,
                        imageUrl = item.Recipe.imageUrl,
                        recipeDescription = item.Recipe.Description
                    });
                }
            }

            return Ok(cartItemDto);
        }


        [HttpGet("getCartItemsOrderd/{orderId}")]
        [Authorize]
        public ActionResult getCartItemsByOrderId(int orderId, int resraurantId)
        {
            Cart cart = IcartRepo.getCatByOrderId(orderId);
            List<CartItem> items = cartItemRepository.GetAllByCartIdAndRestaurantId(cart.id, resraurantId);
            List<CartItemDto> cartItemDto = new List<CartItemDto>();

            foreach (var item in items)
            {
                if (item.Cart.OrderId != null)
                {
                    cartItemDto.Add(new CartItemDto()
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        recipeName = item.Recipe.name,
                        restaurantName = item.Resturant.Name,
                        TotalPrice = item.Recipe.Price * item.Quantity, 
                        recipePrice = item.Recipe.Price,
                        imageUrl = item.Recipe.imageUrl,
                        recipeDescription = item.Recipe.Description
                    });
                }
            }

            return Ok(cartItemDto);
        }


        [HttpGet("getOrderItemsByOrderId")]
        [Authorize]
        public ActionResult getOrderItemsByOrderId(int orderId)
        {
            string AppId = GetUserIdFromClaims();
            Resturant resturant = resturantRepository.getByAppId(AppId);
            Cart cart = IcartRepo.getCatByOrderId(orderId);
            List<CartItem> items = cartItemRepository.GetAllByCartIdAndRestaurantId(cart.id, resturant.id);
            List<CartItemDto> cartItemDto = new List<CartItemDto>();

            foreach (var item in items)
            {
                if (item.Cart.OrderId != null)
                {
                    cartItemDto.Add(new CartItemDto()
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        recipeName = item.Recipe.name,
                        restaurantName = item.Resturant.Name,
                        TotalPrice = item.TotalPrice,
                        recipePrice = item.Recipe.Price,
                        imageUrl = item.Recipe.imageUrl,
                        recipeDescription = item.Recipe.Description
                    });
                }
            }

            return Ok(cartItemDto);
        }




        [HttpPut]
        [Authorize]
        public ActionResult updateCartItem( [FromBody] CartItemDto cartItemDto)
        {
            CartItem cartItem = cartItemRepository.GetById(cartItemDto.Id);
           
            if (cartItem == null)
                return NotFound("CartItem Not Found!");

            cartItem.TotalPrice = cartItemDto.TotalPrice;
            cartItem.Quantity = cartItemDto.Quantity;


            cartItemRepository.Update(cartItem);
            int Raws = cartItemRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("CartItem updated failed.");
        }
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult deleteCartItem(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid CartItem id.");
            }

            var res = cartItemRepository.GetById(id);
            if (res == null)
                return NotFound("CartItem Not Found!");

            cartItemRepository.Delete(id);
            int Raws = cartItemRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }

            return NotFound("CartItem updated failed.");
        }

        [HttpDelete("clearCart")]
        [Authorize]
        public ActionResult clearCart()
        {
            int userId = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;

            Cart cart = IcartRepo.getCartByUserId(userId);
            if (cart == null)
                return BadRequest();
            List<CartItem> cartItems = cartItemRepository.GetAllByCartId(cart.id);
            foreach (var item in cartItems)
            {
                cartItemRepository.Delete(item.Id);
            }
            cartItemRepository.SaveChanges();

            return Ok("deleted");

        }


        [Authorize]
        [HttpPost]
        public ActionResult AddCartItemToCart(PostCartItemDto postCartItemDto)
        {
            int userId = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;
            Cart cart = IcartRepo.GetNonOrderedCartByUserId(userId);
            if (cart == null)
            {
                Cart cartUser = new Cart()
                {
                    userId = userId
                };
                IcartRepo.Add(cartUser);
                IcartRepo.SaveChanges();
                cart = IcartRepo.GetNonOrderedCartByUserId(userId);
            }

            CartItem cartItem = new CartItem()
            {
                Quantity= postCartItemDto.Quantity,
                TotalPrice= postCartItemDto.TotalPrice,
                CartId= cart.id,
                RecipeId= postCartItemDto.RecipeId,
                ResturantId= postCartItemDto.RestaurantId
            };
            cartItemRepository.Add(cartItem);
            cartItemRepository.SaveChanges();
            return Ok("cart item added succesfully to cart");
        }


        internal decimal getTotalPriceOrderByRestaurantIdAndOrderId(int orderId, int restaurantId)
        {
            Cart cart = IcartRepo.getCatByOrderId(orderId);
            return cartItemRepository.getTotalPriceOrderByRestaurantIdAndOrderId(cart.id, restaurantId);

        }

    }
}
