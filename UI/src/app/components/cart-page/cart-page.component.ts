import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from 'src/app/services/ShoppingCart.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css'],
})
export class CartPageComponent implements OnInit {
  cartItems: any[] = [];

  constructor(private cartService: ShoppingCartService) {}

  ngOnInit() {
    this.cartService.getCartItems().subscribe((items) => {
      this.cartItems = items;
    });
  }
}


