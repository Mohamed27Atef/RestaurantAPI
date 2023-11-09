import { Component, OnInit,Input } from '@angular/core';
import { FeedbackService } from 'src/app/services/restaurant-feedback.service'; // Replace with the correct path
import { IRestaurantFeedback } from 'src/app/interfaces/RestaurantFeedback'; 
@Component({
  selector: 'app-feedback-reviews',
  templateUrl: './feedback-reviews.component.html',
  styleUrls: ['./feedback-reviews.component.css'],
})
export class FeedbackReviewsComponent implements OnInit {
  testimonials: IRestaurantFeedback[] = [];
  @Input() userName!: string;
  @Input() userAvatar!: string;
  @Input()restaurantId!:number;
  constructor(private feedbackService: FeedbackService) {}

  ngOnInit() {
    this.feedbackService.getReviewsForRestaurant(this.restaurantId).subscribe((data: IRestaurantFeedback[]) => {
      
      this.testimonials = data;
      console.log(data);
      console.log(this.testimonials);

    });
  }
}

