import { AfterViewInit, Component,Input,OnInit } from '@angular/core';

@Component({
  selector: 'restaurant-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements AfterViewInit{



  constructor() { 
  }
  ngAfterViewInit(): void {
    this.generateStarsArray();

    const todayIndex = new Date().getDay(); 
    const listItems = document.querySelectorAll('.opening-hours li');

    if (listItems && listItems.length > todayIndex) {
      listItems[todayIndex].classList.add('today');
    }
  }
  @Input() rate!: number;
  @Input() name!: string;
  starsArray: number[] = [];
  

  generateStarsArray() {
    this.starsArray = Array.from({ length: Math.floor(this.rate) });
  }

  @Input() closingDays!: string[];
  @Input() oppeningHour!: number;
  @Input() closingHour!: number;

  WeekDays: string[] = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ]

  checkifClosed(day : string) {
    return this.closingDays.find(r => r == day)? true: false;
  }
}


