import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.dev';
import { HeaderService } from './header.service';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  constructor(private myClient: HttpClient, private headerService: HeaderService) {}
  private apiPort = environment.apiPort;

  private DB_URL = `https://localhost:${this.apiPort}/api/Order`;

  GetOrders() {
    return this.myClient.get(this.DB_URL);
  }
  getUserOrder() : Observable<any>{
    const headers = this.headerService.getHeader();
    return this.myClient.get(this.DB_URL + "/getAllOrderOfUser", {headers});
  }
}
