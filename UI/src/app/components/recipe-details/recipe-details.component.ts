import { Component, AfterViewInit, OnInit, ViewChild, ElementRef,Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { data } from 'isotope-layout';
import { Recipe } from 'src/app/interfaces/recipe';
import { RecipeFeedbackService } from 'src/app/services/recipe-feedback.service';
import { RecipeService } from 'src/app/services/recipe.service';
import { AddToCartService } from 'src/app/services/add-to-cart.service';
import { IsAddedToCartService } from 'src/app/services/is-added-to-cart.service';
import { FeedbackAddedService } from 'src/app/services/feedback-added.service';
import { getCookie } from 'typescript-cookie';
import jwtDecode from 'jwt-decode';
import { RecipeFeedbackComponent } from '../recipe-feedback/recipe-feedback.component';
import { RecipeFeddbackReviewsComponent } from '../recipe-feddback-reviews/recipe-feddback-reviews.component';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css'],
})
export class RecipeDetailsComponent implements OnInit, AfterViewInit {
  recipe!: any;
  relatedRecipe: any;
  quantity: number = 1;
  numberOfReview: number = 0;
  @ViewChild("recipeFeedBack") recipeFeedBackChild!: RecipeFeddbackReviewsComponent;
  Id: number = 0;
  recipeAddedToCart:boolean=false;
  feedbackAddedFromUser:boolean=true;
  constructor(
    private myService: RecipeService,
    private myActive: ActivatedRoute,
    private addToCartService : AddToCartService,
    private recipeFeedbackService: RecipeFeedbackService,
    private isAddedToCartService:IsAddedToCartService,
    private  feedbackAddedService:FeedbackAddedService
  ) {
    this.Id = this.myActive.snapshot.params['id'];
  }

  addToCart(){
    this.recipeAddedToCart=true
    console.log( this.recipe)
    const CartItemData={
       quantity: this.quantity,
       totalPrice: this.recipe.price * this.quantity,
       recipeId: this.Id.toString(),
       restaurantId: this.recipe.restaurantId
     }
     this.addToCartService.AddRecipeToCart(CartItemData).subscribe({
       next:(Response)=>{console.log(Response)},
       error:(err)=>console.log(err)
     })   
   }
   submitFeedBackOne(feedback: any) {
    this.recipeFeedBackChild.filteredReviews.push(feedback);
   }
   jsonTokenWithoutDecode: any
   userImage!: any;
   name!: any;
  ngOnInit(): void {
    this.jsonTokenWithoutDecode = getCookie('User');
    this.userImage = getCookie('UserImage');
    let Token: any = jwtDecode(this.jsonTokenWithoutDecode);
    this.name = Token['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
    // this.recipeFeedbackService.getNumberOfReivew(this.Id).subscribe({
    //   next: data=> 
    //     this.numberOfReview = data
    // })
    this.myService.getRecipe(this.Id).subscribe({
      next: (data) => this.recipe = data,
      error: (err) => console.log(err),
    });
    this.myService.getRecipeByMenuId(this.Id).subscribe({
      next: (data) => this.relatedRecipe = data,
      error: (err) => console.log(err),
    });
    this.isAddedToCartService.checkIfAddedToCart(this.Id).subscribe(
      {
        next: (data) =>{ this.recipeAddedToCart=data
          console.log("addedToCartFeomDataBase"+data)},
        error: (err) => console.log(err),
      })

      this.feedbackAddedService.checkIfFeedbackAddedToRecipe(this.Id).subscribe(
        {
          next: (data) =>{ this.feedbackAddedFromUser=data
            console.log("feedbackFromDataBase"+data)},
          error: (err) => console.log(err),
        })

  }

  ngAfterViewInit() {
    const favoriteIcons = document.querySelectorAll('.favorite-button');
    const thumbnailImages = document.querySelectorAll('.thumbnail');
    const mainImage: any = document.querySelector('.main-image img');

    thumbnailImages.forEach((thumbnail: any) => {
      thumbnail.addEventListener('mouseenter', () => {
        const imgSrc = thumbnail.getAttribute('src');
        mainImage.setAttribute('src', imgSrc);
      });
    });

    const wishlistBtn = document.getElementById('wishlistBtn');

    favoriteIcons.forEach((icon) => {
      wishlistBtn?.addEventListener('click', () => {
        icon.classList.toggle('fas');
      });
    });

    this.updateStarRating();
  }

 

  updateStarRating() {
    const productRatingElements = document.querySelectorAll('.product-rating');

    productRatingElements.forEach((ratingElement: any) => {
      const ratingText = ratingElement.querySelector('.stars-num').textContent;
      const rating = parseFloat(ratingText);

      function RoundToNearestHalf(num: number) {
        const rounded = Math.round(num * 2) / 2;
        return rounded; 
      }

      const starsElement = ratingElement.querySelector('.stars');
      starsElement.innerHTML = '';

      for (let i = 1; i <= rating; i++) {
        const starIcon = document.createElement('i');
        starIcon.classList.add('fas', 'fa-star');
        starsElement.appendChild(starIcon);
      }

      if (rating % 1 == 0.5) {
        const halfStarIcon = document.createElement('i');
        halfStarIcon.classList.add('fas', 'fa-star-half-alt');
        starsElement.appendChild(halfStarIcon);
      }

      const emptyStarsCount = 5 - Math.ceil(rating);
      for (let i = 0; i < emptyStarsCount; i++) {
        const starIcon = document.createElement('i');
        starIcon.classList.add('far', 'fa-star');
        starsElement.appendChild(starIcon);
      }
    });
  }

  plus() {
   this.quantity ++;
  }

  remove() {

    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
