import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';
import { CartItem } from '../interfaces/CartItem';
import { OrderDetails } from '../interfaces/order-details';

const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});

@Injectable({
  providedIn: 'root',
})
export class OrderDetailsService {
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/CartItem`;
  constructor(private myClient: HttpClient) {}

  getAllCartItems(): Observable<any> {
    return this.myClient.get(`${this.DB_URL}`, { headers });
  }
  updateCartItem(cart: OrderDetails) {
    return this.myClient.put(this.DB_URL, cart, { headers });
  }
  deleteCartItem(id: Number) {
    return this.myClient.delete(this.DB_URL + '?id=' + id, { headers });
  }
}
