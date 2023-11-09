export interface OrderDetails {
  id: number;
  name: string;
  totalPrice: number;
  quantity: number;
  imageUrl: string;
  isChanged?: boolean;
  recipePrice: number;
  restaurantName: string;
  recipeName: string;
  recipeDescription: string;
  recipeId: number
}
