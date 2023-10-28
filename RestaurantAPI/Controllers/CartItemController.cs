using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                cartItemDto.Add(new CartItemDto() { 
                    Id = item.Id,
                    Quantity = item.Quantity,
                    recipeName = item.Recipe.name,
                    restaurantName = item.Resturant.Name,
                    TotalPrice = item.TotalPrice,
                });
            }

            return Ok(cartItemDto);
        }
    }
}
