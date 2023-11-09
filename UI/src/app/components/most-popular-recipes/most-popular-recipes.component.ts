import { Component,Input,OnInit } from '@angular/core';
import { RecipeService } from 'src/app/services/recipe.service';
import { MostPopularRecipe } from 'src/app/interfaces/MostPopularRecipe';

@Component({
  selector: 'app-most-popular-recipes',
  templateUrl: './most-popular-recipes.component.html',
  styleUrls: ['./most-popular-recipes.component.css']
})
export class MostPopularRecipesComponent implements OnInit {
  recipes: MostPopularRecipe[] = [];

  constructor(private recipeService: RecipeService) { }

  ngOnInit() {
    this.recipeService.getMostPopularRecipes().subscribe(
      (data: MostPopularRecipe[]) => {
        
        this.recipes = data;
        console.log(data);

      },
      error => {
        console.error('Error fetching recipes:', error);
      }
    );
  }

    

    selectedIndex=0;

    showPrev(i:number){
      if(this.selectedIndex>0){
        this.selectedIndex=i-1;
      }

    }
    showNext(i:number){
      if(this.selectedIndex<this.recipes?.length-1){
        this.selectedIndex=i+1;
      }

    }

}


