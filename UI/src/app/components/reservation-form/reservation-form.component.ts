import { Component,Output,EventEmitter,Input, OnInit } from '@angular/core';
import { Table } from 'src/app/interfaces/table';
import { TableService } from 'src/app/services/table.service';
import { FormGroup,FormControl, Validators } from '@angular/forms';
import { error } from 'jquery';
import { Duration } from 'src/app/interfaces/duration';
@Component({ 
  selector: 'app-reservation-form',
  templateUrl: './reservation-form.component.html',
  styleUrls: ['./reservation-form.component.css']
})
export class ReservationFormComponent {
  tables:Table[] = [];
  isDateTimeSelected: boolean = false;
  isSelectClicked: boolean = false;
 @Input() RestaurantId:number=1;
 @Output() submitSecond = new EventEmitter<void>();
 @Output() backdata = new EventEmitter<void>();
 durations:Duration[]=[{hours:1,description:" 1 hour for 5$"},
                      {hours:2,description:" 2 hours for 10$"},
                      {hours:3,description:" 3 hours for 15$"}]
                    

 constructor(private _TableService:TableService){}
 


reservationForm:FormGroup=new FormGroup({
  firstName:new FormControl(null,[Validators.required,Validators.minLength(3)]), 
  phone:new FormControl(null,[Validators.required,Validators.pattern(/^01[0125][0-9]{8}$/)]), 
  date:new FormControl(null,[Validators.required]), 
  time:new FormControl(null,[Validators.required]), 
  tableType:new FormControl(null,[Validators.required]),
  dur:new FormControl(null,[Validators.required])
},{validators:this.dateTimeValidator})


dateTimeValidator(reservationForm:any) {
  const dateControl = reservationForm.get('date');
  const timeControl = reservationForm.get('time');

  if (dateControl.value && timeControl.value) {
    const selectedDateTime = new Date(`${dateControl.value} ${timeControl.value}`);
    const currentDateTime = new Date();

    if (selectedDateTime <= currentDateTime) {
      return { dateTimeInvalid: true };
    }
  }
  return null;
}

onDateTimeChange() {
  if (this.reservationForm.get('date')&& this.reservationForm.get('time')) {
    var t : Date  = new Date(`${this.reservationForm.value.date}T${this.reservationForm.value.time}`);
    t.setHours(Number(t.getHours() + 2)); 
    const dateTime=t;
    const currentDateTime = new Date();
    this.isDateTimeSelected = false;
    if(dateTime > currentDateTime){
      this.isDateTimeSelected = true;
      const ApidateTime=t.toISOString();
    
      this._TableService.getTableByRestaurantIdAndDAteTime(ApidateTime,this.RestaurantId).subscribe((data)=>{
        this.tables=data
        
        console.log();
      }) 
    }
  }  
}

onSelectClicked(){
  if (!this.isDateTimeSelected) {
    this.isSelectClicked = true;
  }
}

 performAction() {
  var t : Date  = new Date(`${this.reservationForm.value.date}T${this.reservationForm.value.time}`);
  t.setHours(Number(t.getHours() + 2)); 
  const reservationData = {
   dateTime:new Date(t).toISOString(),
    restaurantId:this.RestaurantId ,
    tableType: this.reservationForm.value.tableType,
    name: this.reservationForm.value.firstName,
    phone: this.reservationForm.value.phone,
    duration:this.reservationForm.value.dur
  };
  this._TableService.reserveTable(reservationData).subscribe({
    next:(Response)=>console.log(Response),
    error:(err)=>console.log(err)
  })
   this.submitSecond.emit();
 }

 getTableType(tableType:string):string{
  return this._TableService.getTableType(tableType)
 }

 Back(){
  this.backdata.emit();
 }
}
