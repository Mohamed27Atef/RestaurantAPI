import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/interfaces/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import {AddToCartService} from 'src/app/services/add-to-cart.service';
import { MenuService } from 'src/app/services/menu.service';
import { Menu } from '@syncfusion/ej2-angular-navigations';


@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css'],
})

export class RecipeComponent implements OnInit {

  recipes: Recipe[] = [];
  selectedCategoryFilter: string = 'All Categories';
  uniqueCategories: Menu[] = [];
  searchText: string = '';
  minRating: number = 0;
  maxRating: number = 5;
  minPrice: number = 0;
  filteredRecipe!: Recipe[];
  maxPrice: number = 1000;
  restaurantFilter: string = '';
  uniqueRestaurants: string[] = [];
  selectCategory: string = '';
  constructor(private recipeService: RecipeService,private addToCartService: AddToCartService, private menuService: MenuService) {
    
  }

  addToCart(recipe: Recipe) {
    console.log(recipe);
    const CartItemData = {
      quantity: 1,
      totalPrice: recipe.price,
      recipeId: recipe.id.toString(),
      restaurantId: recipe.restaurantId,
    };

    this.addToCartService.AddRecipeToCart(CartItemData).subscribe({
      next:(Response)=>console.log(Response),
      error:(err)=>console.log(err)
    })   
  }

  ngOnInit() {
    
    this.menuService.getMenu().subscribe({
      next:data =>
      { 
        this.uniqueCategories = data
      }
    })
    this.recipeService.getRecipes().subscribe((recipes) => {
      this.recipes = recipes;
      this.uniqueRestaurants = this.getUniqueRestaurants();
    });
  }

  private getUniqueCategories(): string[] {
    const uniqueCategories = [
      ...new Set(this.recipes.map((recipe) => recipe.category)),
    ];
    return uniqueCategories;
  }


  private getUniqueRestaurants(): string[] {
    const uniqueNames = [
      ...new Set(this.recipes.map((recipe) => recipe.restaurantName)),
    ];
    return uniqueNames;
  }

  filterByRestaurant(restaurant: string) {
    //this.restaurantFilter = restaurant;
    this.filterRecipes();
  }


  filterCategory(){
    this.filteredRecipe = this.recipes.filter((recipe) =>
      recipe.rate >= this.minRating && recipe.rate <= this.maxRating
        && recipe.price >= this.minPrice && recipe.price <= this.maxPrice
        && recipe.menuName == this.selectCategory
    );
  }



  selectedCategory(selectedCategory: any) {
    this.selectCategory = selectedCategory.target.value;
    this.filterCategory();
  } 


  filterRecipes() {
    if(this.selectCategory == ''){
      this.filteredRecipe = this.recipes.filter((recipe) =>
        recipe.rate >= this.minRating && recipe.rate <= this.maxRating
          && recipe.price >= this.minPrice && recipe.price <= this.maxPrice
      );
    }else  this.filterCategory();

  }


  searchRecipesByName() {
    console.log(this.searchText)
    if(this.searchText)
    {
      this.recipeService.searchRecipesByName(this.searchText).subscribe((recipes) => {
        this.recipes = recipes;
        this.filteredRecipe = recipes;
      });  
    }    
    else
      this.filteredRecipe = [];

  }
}
