import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserTableServicesService } from 'src/app/services/user-table.service';

@Component({
  selector: 'app-search-table',
  templateUrl: './search-table.component.html',
  styleUrls: ['./search-table.component.css'],
})
export class SearchTableComponent implements OnInit {
  restaurantNumber: string = '';
  restaurantName: string = '';
  searchDate: string = '';
  reservationResult: any[] = [];
  @Output() myEvent = new EventEmitter();

  constructor(private userTableService: UserTableServicesService) {}
  ngOnInit(): void {
    this.getAllReservation();
  }
  getAllReservation() {
    this.userTableService.getAllUserReservation().subscribe({
      next: (data) => {
        this.reservationResult = data;
        this.myEvent.emit(this.reservationResult);
      },
      error: (err) => console.log(err),
    });
  }
  onTableNumberChange() {
    this.getAllReservation();
  }
  searchByTableNumber() {
    this.reservationResult = [];
    this.userTableService.getAllUserReservation().subscribe({
      next: (data) => {
        for (let i = 0; i < data.length; i++) {
          if (data[i].tableNumber == this.restaurantNumber)
            this.reservationResult.push(data[i]);
        }
        this.myEvent.emit(this.reservationResult);
      },
      error: (err) => console.log(err),
    });
  }
  searchByRestaurantName() {
    this.reservationResult = [];
    this.userTableService.getAllUserReservation().subscribe({
      next: (data) => {
        let name = ` ِ${this.restaurantName}`;
        for (let i = 0; i < data.length; i++) {
          const originalString = this.restaurantName;
          if (
            data[i].restaurantName
              .toLowerCase()
              .includes(this.restaurantName.toLowerCase())
          )
            this.reservationResult.push(data[i]);
        }
        this.myEvent.emit(this.reservationResult);
      },
      error: (err) => console.log(err),
    });
  }
  searchByDate() {
    if (this.searchDate) {
      this.reservationResult = [];
      this.userTableService.getAllUserReservation().subscribe({
        next: (data) => {
          for (let i = 0; i < data.length; i++) {
            if (data[i].dateTime.split('T')[0] == this.searchDate)
              this.reservationResult.push(data[i]);
          }
          this.myEvent.emit(this.reservationResult);
        },
        error: (err) => console.log(err),
      });
    } else {
      this.getAllReservation();
    }
  }

  clearDate() {
    this.getAllReservation();
    this.searchDate = '';
  }
}
