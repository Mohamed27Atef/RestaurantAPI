import { Component } from '@angular/core';

@Component({
  selector: 'app-reservation-container',
  templateUrl: './reservation-container.component.html',
  styleUrls: ['./reservation-container.component.css']
})
export class ReservationContainerComponent {
  showFirstForm = true;
  showSecondForm = false;
  selectedRestaurantId:number=0;
  handleChangeDataEvent(data:number) {
    this.showFirstForm = false;
    this.showSecondForm =true;
    this.selectedRestaurantId=data;
    console.log(this.selectedRestaurantId)
  } 
  handlesubmitSecond(){
    this.showFirstForm = true;
    this.showSecondForm =false;  
  }
  handleBack(){
    this.showFirstForm = true;
    this.showSecondForm =false;  
  }
}
