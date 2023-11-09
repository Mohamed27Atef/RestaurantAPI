import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';
import { OrderdAddress } from '../interfaces/orderd-address';

const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});

@Injectable({
  providedIn: 'root',
})
export class CreateorderService {
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/Order`;
  constructor(private myClient: HttpClient) {}

  postCartItem(order: OrderdAddress) {
    return this.myClient.post(this.DB_URL, order, { headers });
  }
}
