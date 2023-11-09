import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FeedbackService } from 'src/app/services/restaurant-feedback.service';
import { IRestaurantFeedback } from 'src/app/interfaces/RestaurantFeedback';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css'],
})
export class FeedbackComponent implements OnInit {
  stars: number[] = [1, 2, 3, 4, 5];
  selectedRating: number = 0;
  @Input() userName!: string;
  @Input() userAvatar!: string;
  @Input() restaurantId!: number;
  @Output() postedReview = new EventEmitter();
  userComment: string = '';
  RemoveComponentAfterSubmit:boolean=false;


  constructor(private feedbackService: FeedbackService) {}

  ngOnInit(): void {
    console.log(this.userName)
  }

  rate(rating: number): void {
    this.selectedRating = rating;
  }

  submitFeedback() {
    this.RemoveComponentAfterSubmit=true;
    const feedbackToAdd = {
      text: this.userComment,
      rate: this.selectedRating,
      postDate: new Date(),
      ResturantId: this.restaurantId,
    };
    this.postedReview.emit(feedbackToAdd);

    this.feedbackService.postFeedback(feedbackToAdd).subscribe(
      {
        next: (response) => {
          console.log('Feedback submitted successfully.', response);
        },
        error: (error) => {
          console.error('Error submitting feedback.', error);
        },
      }
    );

    // Clear the form
    this.userComment = '';
    this.selectedRating = 0;
  }
}
