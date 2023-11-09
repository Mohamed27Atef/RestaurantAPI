import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CartItem } from 'src/app/interfaces/CartItem';
import { OrderDetails } from 'src/app/interfaces/order-details';
import { CartitemService } from 'src/app/services/cartitem.service';
import { OrderDetailsService } from 'src/app/services/order-details.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css'],
})
export class OrderDetailsComponent implements OnInit {
  CartItems!: OrderDetails[];
  constructor(
    private orderdetailService: OrderDetailsService,
    private router: Router,
    private cartItemService: CartitemService
  ) {}

  ngOnInit(): void {
    this.orderdetailService.getAllCartItems().subscribe({
      next: (data) => {
        this.CartItems = data;
      },
      error: (err) => console.log(err),
    });
  }

  decreaseQuantity(item: OrderDetails) {
    if (item.quantity > 1) {
      item.quantity--;
      item.totalPrice -= item.recipePrice;
      item.isChanged = true;
    }
  }

  increaseQuantity(item: OrderDetails) {
    item.quantity++;
    item.totalPrice += item.recipePrice;
    item.isChanged = true;
  }

  calculateTotalPrice() {
    return this.CartItems.reduce((total, item) => total + item.totalPrice, 0);
  }

  updateCartItem() {
    const updateObservables = this.CartItems.filter(
      (cartItem) => cartItem.isChanged
    ).map((cartItem) => {
      return this.orderdetailService.updateCartItem(cartItem);
    });

    forkJoin(updateObservables).subscribe({
      next: (results) => {
        console.log('All cart items updated successfully');
      },
      error: (error) => {
        console.error('Error updating cart items:', error);
      },
    });
  }

  RemoveCartItem(item: any) {
    this.CartItems.splice(
      this.CartItems.findIndex((r) => r.id == item.id),
      1
    );
    this.cartItemService.deleteCartItem(item.id).subscribe({
      next: (results) => {
        console.log(' cart items deleted successfully');
      },
      error: (error) => {
        console.error('Error deleting cart items:', error);
      },
    });
  }
}
