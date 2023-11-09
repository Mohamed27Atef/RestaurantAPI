import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.dev';
import { HeaderService } from './header.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartitemService {

  private apiPort = environment.apiPort;
  private BaseUrl: string = `https://localhost:${this.apiPort}/api/CartItem`;
  
  constructor(private httpClient: HttpClient, private headerService: HeaderService) { }

  deleteCartItem(id: number) {
    const headers = this.headerService.getHeader();
    return this.httpClient.delete(this.BaseUrl + "/" + id , {headers});
  }
  clearCart() {
    const headers = this.headerService.getHeader();
    return this.httpClient.delete(this.BaseUrl + "/clearCart", {headers});
  }
  getOrderDetails(orderId: number, restaurantId: number): Observable<any> {
    const headers = this.headerService.getHeader();
    return this.httpClient.get(this.BaseUrl + "/getCartItemsOrderd/" + orderId+"?resraurantId=" + restaurantId, {headers});
  
  }
}
