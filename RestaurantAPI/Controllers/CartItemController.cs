using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.Cart;
using RestaurantAPI.Dto.CartItem;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class CartItemController : BaseApiClass
    {
        private readonly ICartUserRrepository cartUserRrepository;
        private readonly IUserRepository userRepository;
        private readonly ICartItemRepository cartItemRepository;

        public CartItemController(ICartUserRrepository cartUserRrepository, 
            IUserRepository userRepository,
            ICartItemRepository cartItemRepository)
        {
            this.cartUserRrepository = cartUserRrepository;
            this.userRepository = userRepository;
            this.cartItemRepository = cartItemRepository;
        }
        [HttpGet]
        [Authorize]
        public ActionResult getCartItems()
        {
            Cart cart = cartUserRrepository.getCartByUserId(userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
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
                        imageUrl = item.Recipe.imageUrl
                    });
                }
            }
                

            return Ok(cartItemDto);
        }

        [HttpGet("getCartItemsOrderd")]
        [Authorize]
        public ActionResult getCartItemsOrderd()
        {
            Cart cart = cartUserRrepository.getCartByUserId(userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);
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
                        imageUrl = item.Recipe.imageUrl
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
                return NotFound("cartItem Not Found!");

            cartItem.TotalPrice = cartItemDto.TotalPrice;
            cartItem.Quantity = cartItemDto.Quantity;


            cartItemRepository.Update(cartItem);
            int Raws = cartItemRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }


            return NotFound("cartItem updated failed.");
        }
    }
}
