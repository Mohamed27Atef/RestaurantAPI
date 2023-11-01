namespace RestaurantAPI.Dto.CartItem
{
    public class PostCartItemDto
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int RecipeId { get; set; }
        public int RestaurantId { get; set; }

    }
}
