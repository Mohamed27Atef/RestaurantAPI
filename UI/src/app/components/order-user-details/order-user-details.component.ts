import jwtDecode from 'jwt-decode';
import { OrderUserDetailsService } from 'src/app/services/order-user-details.service';
import { getCookie } from 'typescript-cookie';

import { Component, Input, OnInit } from '@angular/core';
import { CartItemsOrder } from 'src/app/interfaces/cart-items-order';
import { CartitemService } from 'src/app/services/cartitem.service';

import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-order-user-details',
  templateUrl: './order-user-details.component.html',
  styleUrls: ['./order-user-details.component.css'],
})
export class OrderUserDetailsComponent implements OnInit {
  orderId!: number;
  userName: string = '';
  orders: any;
  status!: string;
  totalPrice!: number;
  restaurantId!: number;

  cartItems!: CartItemsOrder[];
  constructor(
    private cartItemService: CartitemService,
    private myService: OrderUserDetailsService,
    private myActive: ActivatedRoute
  ) {

    let jsonTokenWithoutDecode: any = getCookie('User');

    let tokenDecoded: any = jwtDecode(jsonTokenWithoutDecode);
    this.userName =
      tokenDecoded[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ];
    this.orderId = this.myActive.snapshot.params['id'];
    this.myActive.queryParams.subscribe(params => {
      this.status = params['status'];
      this.totalPrice = params['totalPrice']
      this.restaurantId = params['retaurantId'];

  });
  }

  ngOnInit(): void {
    this.cartItemService.getOrderDetails(this.orderId,this.restaurantId ).subscribe({
      next: (data) => (this.cartItems = data),
    });
    this.myService.GetOrderById(this.orderId).subscribe({
      next: (data) => {
        this.orders = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
