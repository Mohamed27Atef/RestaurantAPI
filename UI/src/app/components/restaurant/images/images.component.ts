import { AfterViewInit, Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'resturamt-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.css']
})
export class ImagesComponent implements AfterViewInit {
  constructor() { }
  @Input("images") images!: string[];
  @Input() mainImage!: string;
  currentIndex: number = 0;

  ngAfterViewInit(): void {
    setInterval(() => this.nextSlide(),3000);
  }

  showSlide(index: number) {
    this.currentIndex = index;
  }

  nextSlide() {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
  }
}
