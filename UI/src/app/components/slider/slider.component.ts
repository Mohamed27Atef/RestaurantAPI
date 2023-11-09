import { Component, OnInit, OnDestroy } from '@angular/core';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css']
})
export class SliderComponent implements OnInit, OnDestroy {
  images: string[] = ['/images/f1.png', '/images/f2.png', '/images/f3.png'];
  currentImageIndex: number = 0;
  private slideshowSubscription: Subscription | undefined;

  ngOnInit() {
    this.startSlideShow();
  }

  ngOnDestroy() {
    this.stopSlideShow();
  }

  nextImage() {
    if (this.currentImageIndex < this.images.length - 1) {
      this.currentImageIndex++;
    }
  }

  previousImage() {
    if (this.currentImageIndex > 0) {
      this.currentImageIndex--;
    }
  }

  startSlideShow() {
    if (!this.slideshowSubscription) {
      this.slideshowSubscription = interval(3000).subscribe(() => {
        if (this.currentImageIndex < this.images.length - 1) {
          this.nextImage();
        } else {
          this.currentImageIndex = 0;
        }
      });
    }
  }

  stopSlideShow() {
    if (this.slideshowSubscription) {
      this.slideshowSubscription.unsubscribe();
      this.slideshowSubscription = undefined;
    }
  }

  selectImage(index: number) {
    this.currentImageIndex = index;
    this.stopSlideShow();
  }
}

