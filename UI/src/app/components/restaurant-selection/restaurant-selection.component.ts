import { Component,Output,EventEmitter } from '@angular/core';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { FormGroup,FormControl, Validators } from '@angular/forms';
@Component({
  selector: 'app-restaurant-selection',
  templateUrl: './restaurant-selection.component.html',
  styleUrls: ['./restaurant-selection.component.css']
})
export class RestaurantSelectionComponent {
  restaurants: Restaurant[] = [];
  constructor(_RestaurantService:RestaurantService){
    _RestaurantService.getAllRestaurant().subscribe((data)=>{
        this.restaurants=data
    })
  }
  selectedId:any;
  @Output() changeDataEvent = new EventEmitter<number>();
 
  selectionForm:FormGroup = new FormGroup({
   rest :new FormControl(null,[Validators.required]) 
  })

  performAction() {
    console.log(this.selectedId)
    this.changeDataEvent.emit(this.selectedId);
  }

}
