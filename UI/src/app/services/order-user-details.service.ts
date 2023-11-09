import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class OrderUserDetailsService {
  constructor(private myClient: HttpClient) {}
  private apiPort = environment.apiPort;

  private DB_URL = `https://localhost:${this.apiPort}/api/Order/`;

  GetOrderById(id: number) {
    return this.myClient.get(this.DB_URL + id);
  }
}
