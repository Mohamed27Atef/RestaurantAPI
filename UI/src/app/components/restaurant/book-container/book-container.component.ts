import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-book-container',
  templateUrl: './book-container.component.html',
  styleUrls: ['./book-container.component.css']
})
export class BookContainerComponent {
  @Input() restaurantId!: number;
  @Input() openHour!: number
  @Input() closeHour!: number
  constructor() {
  }
  showSecondForm = false;
  handleChangeDataEvent() {
    this.showSecondForm =true;
  } 
  handlesubmitSecond(){
    this.showSecondForm =false;  
  }
  handleBack(){
    this.showSecondForm =false;  
  }
}
