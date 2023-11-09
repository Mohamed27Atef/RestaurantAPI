import { Component, OnInit } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { Restaurant } from 'src/app/interfaces/restaurant';

import { UserOrders } from 'src/app/interfaces/user-orders';

import { OrdersService } from 'src/app/services/orders.service';
import { RestaurantService } from 'src/app/services/restaurant.service';
import { getCookie } from 'typescript-cookie';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  orders!: UserOrders[];
  statusColor: string = 'bg-success';

  filteredOrders: UserOrders[] = [];
  filterStatus: string = '';
  filterRestaurant: string = '';
  filterDate: string = '';

  restaurants: Restaurant[] = [];
  status: string[] = ['processed', 'shipped', 'enRoute', 'arrived', 'Canceled'];
  selectedPage:number=1;

  constructor(
    private myService: OrdersService,
    private _RestaurantService: RestaurantService,
    private datePipe: DatePipe
  ) {
    let user = getCookie('User');
  }

  formatDates() {
    for (const obj of this.filteredOrders) {
      const formattedDate = this.datePipe.transform(obj.createdAt, 'dd/MM/yyyy hh:mm a');
      obj.formattedDateTime = formattedDate;
      console.log(formattedDate)
    }
  }
  ngOnInit(): void {
    this._RestaurantService.getAllRestaurant().subscribe((data) => {
      this.restaurants = data;
    });

    this.myService.getUserOrder().subscribe({
      next: (data) => {
        (this.orders = data), (this.filteredOrders = data);
        this.formatDates();
      },
      error: (err) => console.log(err),
    });
  }

  handleChangeDataEvent(data:number){
    this.selectedPage=data;
    
    this.myService.getUserOrder().subscribe({
      next: (data) => {
        (this.orders = data), (this.filteredOrders = data);
      },
      error: (err) => console.log(err),
    });
    }

  applyFilters() {
    this.filteredOrders = this.orders.filter((order) => {
      const statusMatch =
        this.filterStatus === '' || order.status === this.filterStatus;
      const restaurantMatch =
        this.filterRestaurant === '' ||
        order.restaurantName
          .toLowerCase()
          .includes(this.filterRestaurant.toLowerCase());
      const dateMatch =
        this.filterDate === '' ||
        order.createdAt
          .toLocaleString()
          .toLowerCase()
          .includes(this.filterDate.toLowerCase());
      return statusMatch && restaurantMatch && dateMatch;
    });
  }

  getSatausColor(status: string) {
    switch (status) {
      case 'processed':
        return 'bg-primary';
      case 'shipped':
        return 'bg-warning';
      case 'enRoute':
        return 'bg-info';
      case 'arrived':
        return 'bg-success';
      case 'Canceled':
        return 'bg-danger';
      default:
        return 'bg-Light';
    }
  }
  clearDate() {
    this.myService.getUserOrder().subscribe({
      next: (data) => {
        (this.orders = data), (this.filteredOrders = data);
      },
      error: (err) => console.log(err),
    });
    this.filterDate = '';
  }
}
