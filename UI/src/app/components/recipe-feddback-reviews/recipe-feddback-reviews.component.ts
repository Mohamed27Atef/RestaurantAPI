import { Component, OnInit, Input, OnChanges, SimpleChanges, AfterViewInit } from '@angular/core';
import { RecipeFeedbackService } from 'src/app/services/recipe-feedback.service';
import { RecipeFeedback } from 'src/app/interfaces/RecipeFeedback';
@Component({
  selector: 'app-recipe-feddback-reviews',
  templateUrl: './recipe-feddback-reviews.component.html',
  styleUrls: ['./recipe-feddback-reviews.component.css']
})
export class RecipeFeddbackReviewsComponent {
  @Input() userName!: string;
  @Input() userAvatar!: string;
  @Input() recipeId!: number;

  feedbackData: any = { reviews: [] };
  filteredReviews: RecipeFeedback[] = []; // Use the RecipeFeedback interface
  ratingCounts: number[] = [0, 0, 0, 0, 0];
  averageRating: number = 0;
  selectedRating: number | null = null;
  totalRating: number = 0;
  constructor(private feedbackService: RecipeFeedbackService) {}



  ngOnInit() {
    this.feedbackService.getReviewsForRecipe(this.recipeId).subscribe(
      (data: RecipeFeedback[]) => {  console.log(this.feedbackData);
        this.feedbackData.reviews = data;
        this.calculateRatingCounts();
        this.filterDisplayedReviews();
        if(data.length>0){
          for (const review of data) {
            this.totalRating += review.rate;
          }
          this.averageRating = this.totalRating / data.length;
      }
      else{this.averageRating=0;}
      }
    );
  }

  calculateRatingCounts() {
    if (this.feedbackData.reviews && this.feedbackData.reviews.length > 0) {
      this.feedbackData.reviews.forEach((review: RecipeFeedback) => {
        const rating = review.rate;
        if (rating >= 1 && rating <= 5) {
          this.ratingCounts[rating - 1]++;
        }
      });
    }
  }

  filterDisplayedReviews() {
    if (this.selectedRating === null) {
      this.filteredReviews = this.feedbackData.reviews;
    } else {
      this.filteredReviews = this.feedbackData.reviews.filter(
        (review: RecipeFeedback) => review.rate === this.selectedRating
      );
    }
  }

  filterReviewsByRating(rating: number) {
    this.selectedRating = rating;
    this.filterDisplayedReviews();
  }
}
