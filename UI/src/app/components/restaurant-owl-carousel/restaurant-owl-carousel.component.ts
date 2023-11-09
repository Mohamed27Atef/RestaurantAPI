import { Component, Input, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { RestaurantService } from 'src/app/services/restaurant.service';

@Component({
  selector: 'app-restaurant-owl-carousel',
  templateUrl: './restaurant-owl-carousel.component.html',
  styleUrls: ['./restaurant-owl-carousel.component.css']
})
export class RestaurantOwlCarouselComponent implements OnInit {
  restaurants!: Restaurant[];

  constructor(private restaurantService: RestaurantService) {}
  
  ngOnInit(): void {
    this.restaurantService.getAllRestaurant().subscribe({
      next: data => this.restaurants = data,
    })
  }


  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: true,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 700,
    navText: ['', ''],
    responsive: {
      0: {
        items: 1
      },
      400: {
        items: 2
      },
      740: {
        items: 3
      },
      940: {
        items: 4
      }
    },
    nav: true
  }


}
