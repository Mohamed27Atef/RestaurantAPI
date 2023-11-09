import { Component, EventEmitter, Output } from '@angular/core';
import { OrderForAdmin } from 'src/app/interfaces/order-for-admin';
import { AdminOrderService } from 'src/app/services/admin-order.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.css'],
})
export class AdminOrdersComponent {
  orders: OrderForAdmin[] = [];
  //
  filteredOrders: OrderForAdmin[] = [];
  filterStatus: string = '';
  filterCustomerName: string = '';
  filterDate: string = '';
  selectedPage:number=1;
  //

  status: string[] = ['processed', 'shipped', 'enRoute', 'arrived', 'Canceled'];

  @Output() changeDataEvent = new EventEmitter<any>();

  constructor(private _AdminOrderService: AdminOrderService,
    private datePipe: DatePipe ) {}

    formatDates() {
      for (const obj of this.filteredOrders) {
        const formattedDate = this.datePipe.transform(obj.date, 'dd/MM/yyyy hh:mm a');
        obj.formattedDateTime = formattedDate;
        console.log(formattedDate)
      }
    }
  ngOnInit() {
    this._AdminOrderService
      .GetOrdersByRestaurantId()
      .subscribe((data) => {
        this.orders = data;
        this.filteredOrders = data;
        console.log(data);
        this.formatDates()
      });
  }
  
  handleChangeDataEvent(data:number){
    this.selectedPage=data;
    this._AdminOrderService
    .GetOrdersByRestaurantId()
    .subscribe((data) => {
      this.orders = data;
      this.filteredOrders = data;
      console.log(data);
    });
    }

  applyFilters() {
    this.filteredOrders = this.orders.filter((order) => {
      const statusMatch =
        this.filterStatus === '' || order.status === this.filterStatus;
      const customerMatch =
        this.filterCustomerName === '' ||
        order.customerName
          .toLowerCase()
          .includes(this.filterCustomerName.toLowerCase());
      const dateMatch =
        this.filterDate === '' ||
        order.date.toLowerCase().includes(this.filterDate.toLowerCase());
      return statusMatch && customerMatch && dateMatch;
    });
  }

  detailsAction(orderWithstatus: any[]) {
    this.changeDataEvent.emit(orderWithstatus);
  }
  onSelectionChange(selection: [string, number]) {
    const selectedValue = selection[0];
    console.log(selectedValue);
    const orderId = selection[1];
    console.log(orderId);
    this._AdminOrderService
      .updateOrderStatusByOrderId(orderId, selectedValue)
      .subscribe({
        next: (Response) => console.log(Response),
        error: (err) => console.log(err),
      });
  }

  // clearDate() {
  //   this._AdminOrderService
  //     .GetOrdersByRestaurantId()
  //     .subscribe((data) => {
  //       this.orders = data;
  //       this.filteredOrders = data;
  //       console.log(data);
  //     });
  //   this.filterDate = '';
  // }
}
