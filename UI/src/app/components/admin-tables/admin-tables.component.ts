import { Component } from '@angular/core';
import { AdminTable } from 'src/app/interfaces/admin-table';
import { AdminReservationsService } from 'src/app/services/admin-reservations.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-tables',
  templateUrl: './admin-tables.component.html',
  styleUrls: ['./admin-tables.component.css']
})
export class AdminTablesComponent {
reservations:AdminTable[]=[] 
filteredReservations: AdminTable[] = [];

filterTableId: string = "";
filterCustomerName: string = "";
filterDate: string = "";
selectedPage:number=1;

constructor(private _AdminReservationsService :AdminReservationsService,
  private datePipe: DatePipe ){}


  formatDates() {
    for (const obj of this.filteredReservations) {
      const formattedDate = this.datePipe.transform(obj.dateTime, 'dd/MM/yyyy hh:mm a');
      obj.formattedDateTime = formattedDate;
      console.log(formattedDate)
    }
  }

ngOnInit(){

  this._AdminReservationsService.GetReservationsByRestaurantId(this.selectedPage).subscribe((data)=>{

    this.reservations=data
    this.filteredReservations = data;
    this.formatDates();
    console.log(this.filteredReservations);
  }) 
 
}


handleChangeDataEvent(data:number){
this.selectedPage=data;
this._AdminReservationsService.GetReservationsByRestaurantId(this.selectedPage).subscribe((data)=>{
  this.reservations=data
  this.filteredReservations = data;
  console.log(data);
})
}

applyFilters() {
  this.filteredReservations = this.reservations.filter((reservation) => {
    const tableIdMatch = this.filterTableId === "" || reservation.tableNumber.toString() === this.filterTableId;
    const customerMatch = this.filterCustomerName === "" || reservation.customerName.toLowerCase().includes(this.filterCustomerName.toLowerCase());
    const dateMatch = this.filterDate === "" || reservation.dateTime.toLowerCase().includes(this.filterDate.toLowerCase());
    return tableIdMatch && customerMatch && dateMatch;
  });
}

placeholderText: string = 'Default Placeholder';

onFocus() {
  this.placeholderText = 'Custom Placeholder';
}

onBlur() {
  this.placeholderText = 'Default Placeholder';
}


}
