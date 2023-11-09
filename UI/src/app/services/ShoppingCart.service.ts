import { Injectable } from '@angular/core';
//TestHere
import { BehaviorSubject, Observable,of } from 'rxjs';
import { CartItem } from '../interfaces/CartItem';
import { Item } from '@syncfusion/ej2-angular-navigations';
import { environment } from 'src/environments/environment.dev';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getCookie } from 'typescript-cookie';


@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/CartItem`;


  constructor(private httpClient: HttpClient){}

  private isCartVisibleSource = new BehaviorSubject<boolean>(false);
  isCartVisible$ = this.isCartVisibleSource.asObservable();
  toggleCartVisibility() {
    this.isCartVisibleSource.next(!this.isCartVisibleSource.value);
  }


  getCartItems(): Observable<any> {
    const JsonToken = getCookie('User');
    const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.httpClient.get(this.DB_URL, {headers});
  }
}








// addToCart(item: CartItem) {
  //   this.cartItems.push(item);
  // }

  // getCartItems() {
  //   return this.cartItems;
  // }

  // incrementCartItem(item: CartItem) {
  //   const foundItem = this.cartItems.find((cartItem) => cartItem.id === item.id);
  //   if (foundItem) {
  //     foundItem.quantity++;
  //   }
  // }

  // decrementCartItem(item: CartItem) {
  //   const existingItem = this.cartItems.find((cartItem) => cartItem.id === item.id);
  //   if (existingItem) {
  //     if (existingItem.quantity > 1) {
  //       existingItem.quantity--;
  //     } else {
  //       const index = this.cartItems.indexOf(existingItem);
  //       if (index !== -1) {
  //         this.cartItems.splice(index, 1);
  //       }
  //     }
  //   }
  // }
  // removeFromCart(item: CartItem) {
  //   const index = this.cartItems.indexOf(item);
  //   if (index !== -1) {
  //     this.cartItems.splice(index, 1);
  //   }
  // }
    // calculateTotalPrice(): number {
  //   return this.cartItems.reduce((total, item) => {
  //     return total + item.quantity * item.price;
  //   }, 0);
  // }
