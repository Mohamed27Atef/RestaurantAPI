import { Component, Input, OnInit } from '@angular/core';
import { MostRatedRecipe } from 'src/app/interfaces/most-rated-recipe';
import { Recipe } from 'src/app/interfaces/recipe';
import { MenuService } from 'src/app/services/menu.service';

@Component({
  selector: 'restaurant-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {

  constructor(private menuService: MenuService){}
  @Input() restaurantId!: number;
  recipes!: MostRatedRecipe[];
  ngOnInit(): void {
    console.log(this.restaurantId)
    this.menuService.getMostRated(this.restaurantId).subscribe({
      next: data=> this.recipes = data
    })
  }


}
