import { Component } from '@angular/core';
import { OrderForAdmin } from 'src/app/interfaces/order-for-admin';

@Component({
  selector: 'app-admin-order-container',
  templateUrl: './admin-order-container.component.html',
  styleUrls: ['./admin-order-container.component.css']
})
export class AdminOrderContainerComponent {
  showTable = true;
  showDiv = false;
  order:any;
  newStatus:string="";

  handleChangeDataEvent(data:any[]) {
    this.showTable= false;
    this.showDiv =true;
    this.order=data[0];
    this.newStatus=data[1]
  } 
 
  handlesubmitSecond(){
    this.showTable = true;
    this.showDiv =false;  
  }
  handleBack(){
    this.showTable = true;
    this.showDiv =false;  
  }
}
