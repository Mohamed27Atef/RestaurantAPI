import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { RestaurantOwlCarouselComponent } from '../restaurant-owl-carousel/restaurant-owl-carousel.component';
import { data } from 'isotope-layout';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{
  @ViewChild("restaurant") restaurantComponent! : RestaurantOwlCarouselComponent;
  categoryId!: number;
  constructor(private restaurantSerivce: RestaurantService){}
  getCategoryId(cat_id : string) {
    this.categoryId = Number(cat_id);
    this.restaurantSerivce.getRestaurantByCategoryId(this.categoryId).subscribe({
      next: data => this.restaurantComponent.restaurants = data,
    })
  }

  getLocatoin(location: string) {
    this.restaurantSerivce.getRestaurantByLocation(location).subscribe({
      next: data => this.restaurantComponent.restaurants = data
    }) 
  }
  images=[
    {imgSrc:"assets/images/o1.jpg" },
    {imgSrc:"assets/images/o2.jpg" },
    {imgSrc:"assets/images/o1.jpg" },
    {imgSrc:"assets/images/o2.jpg" },
  ]
  
}


  