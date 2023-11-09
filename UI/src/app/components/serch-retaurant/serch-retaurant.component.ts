import { Component } from '@angular/core';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { RestaurantService } from 'src/app/services/restaurant.service';


@Component({
  selector: 'app-serch-retaurant',
  templateUrl: './serch-retaurant.component.html',
  styleUrls: ['./serch-retaurant.component.css']
})
export class SerchRetaurantComponent {
  isSearch: boolean = false;
  restaurantResult: Restaurant[] = [];
  constructor(private RestaurantService: RestaurantService) {
    
  }


  searchOfRestaurantByName(name : string) {
    this.isSearch = true
    if(name == ""){
      this.getAllRestaurant();
      this.isSearch = false;
    }
    else
      this.getRestaurantByName(name);
  }

  getRestaurantByName(q: string){
    this.RestaurantService.getRestaurantByName(q).subscribe({
      next: data => this.restaurantResult = data
    })
  }

  getAllRestaurant() {
    this.RestaurantService.getAllRestaurant().subscribe({
      next: data => this.restaurantResult = data
    })
  }

}
