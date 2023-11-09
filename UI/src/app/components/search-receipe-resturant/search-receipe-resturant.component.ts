import { Component } from '@angular/core';
import { Recipe } from 'src/app/interfaces/recipe';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-search-receipe-resturant',
  templateUrl: './search-receipe-resturant.component.html',
  styleUrls: ['./search-receipe-resturant.component.css'],
})
export class SearchReceipeResturantComponent {
  recipeName: string = '';
  filteredRecipe!: Recipe[];
  constructor(private myReceipe: RecipeService) {
    console.log(this.recipeName);
  }

  ngOnChanges() {
    this.myReceipe.searchRecipeInResByName(this.recipeName).subscribe({
      next: (data) => {
        console.log(data);
        this.filteredRecipe = data;
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
}
