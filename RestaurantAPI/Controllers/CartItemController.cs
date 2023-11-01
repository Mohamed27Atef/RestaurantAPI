using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto.Cart;
using RestaurantAPI.Dto.CartItem;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.CartRepository;

namespace RestaurantAPI.Controllers
{
    public class CartItemController : BaseApiClass
    {
        private readonly ICartUserRrepository cartUserRrepository;
        private readonly IUserRepository userRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly ICartRepository IcartRepo;

        public CartItemController(ICartUserRrepository cartUserRrepository, 
            IUserRepository userRepository,
            ICartItemRepository cartItemRepository,
            ICartRepository IcartRepo)
        {
            this.cartUserRrepository = cartUserRrepository;
            this.userRepository = userRepository;
            this.cartItemRepository = cartItemRepository;
            this.IcartRepo = IcartRepo;
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
                        imageUrl = item.Recipe.imageUrl,
                        recipeDescription = item.Recipe.Description
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
        [HttpDelete]
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

       
        [Authorize]
        [HttpPost]
        public ActionResult AddCartItemToCart(PostCartItemDto postCartItemDto)
        {
            int userId = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;
            Cart cart = cartUserRrepository.GetNonOrderedCartByUserId(userId);
            int cartId;
            if (cart == null)
            {
                //create new cart
                Cart newCart = new Cart();
                IcartRepo.Add(newCart);
                IcartRepo.SaveChanges();
                cartId = newCart.id;
                CartUser cartUser = new CartUser()
                {
                    user_id = userId,
                    cart_id = cartId
                };
                cartUserRrepository.Add(cartUser);
                cartUserRrepository.SaveChanges();
            }
            else
            {
                //adding cart item to this cart 
                cartId = cart.id;
            }

            CartItem cartItem = new CartItem()
            {
                Quantity= postCartItemDto.Quantity,
                TotalPrice= postCartItemDto.TotalPrice,
                CartId= cartId,
                RecipeId= postCartItemDto.RecipeId,
                ResturantId= postCartItemDto.RestaurantId
            };
            cartItemRepository.Add(cartItem);
            cartItemRepository.SaveChanges();
            return Ok("cart item added succesfully to cart");
        }

    }
}
