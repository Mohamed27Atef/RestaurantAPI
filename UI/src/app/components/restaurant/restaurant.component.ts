import { AfterContentInit, AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { data } from 'isotope-layout';
import { Menu } from 'src/app/interfaces/menu';
import { Recipe } from 'src/app/interfaces/recipe';
import { RestaurantInfo } from 'src/app/interfaces/restaurant-info';
import { MenuService } from 'src/app/services/menu.service';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { MenuComponent } from './menu/menu.component';
import { ActivatedRoute, TitleStrategy } from '@angular/router';
import { getCookie } from 'typescript-cookie';
import jwtDecode from 'jwt-decode';
import { FeedbackAddedService } from 'src/app/services/feedback-added.service';
import { FeedbackScoreComponent } from '../feedback-score/feedback-score.component';
 

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit {

  currentRestaurant!: RestaurantInfo;
  id!: number;
  images!: string[];
  name!: string;
  userImage!: any;
  jsonTokenWithoutDecode!: any;
  @ViewChild("feedBackScoreComponent") feedBackScoreChild!: FeedbackScoreComponent
 
  looding: boolean = false
  feedbackAddedFromUser:boolean=true;
  @ViewChild("menu") menuCompent!: MenuComponent;
  constructor(private restaurantService: RestaurantService,
     private menuService: MenuService,
      activeRoute : ActivatedRoute,
      private  feedbackAddedService:FeedbackAddedService){
    this.id =activeRoute.snapshot.params["id"];

  }

  postReivew(postedReivew: any){
    this.feedBackScoreChild.filteredReviews.push(postedReivew)
  }



  ngOnInit(): void {
    this.feedbackAddedService.checkIfFeedbackAddedToRestaurant(this.id).subscribe(
      {
        next: (data) =>{ this.feedbackAddedFromUser=data
          console.log("feedbackFromDataBase"+data)},
        error: (err) => console.log(err),
      })
    ///// get name and image
    this.jsonTokenWithoutDecode = getCookie('User');
    this.userImage = getCookie('UserImage');
    let Token: any = jwtDecode(this.jsonTokenWithoutDecode);
    this.name = Token['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']


    ///////////////////////////////////////////

    this.restaurantService.getRestaurantById(this.id).subscribe({
      next: data => {this.currentRestaurant = data; console.log(data)}
    })
    this.restaurantService.getRestaurantImages(this.id).subscribe({
      next: data => this.images = data
    })
    this.menuService.getMenuByRestaurnatId(this.id).subscribe({
      
      next: data => {
        this.looding= true;
        this.menuCompent.menus = data.menuDto;
        const recipes = data.recipeDtos;
        console.log(data)
      const midIndex = Math.floor(recipes.length / 2);
      this.menuCompent.recipe1 = recipes.slice(0, midIndex);
      this.menuCompent.recipe2 = recipes.slice(midIndex);

      }
    })
    
  }

}
