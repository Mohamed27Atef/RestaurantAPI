import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';
import { HeaderService } from './header.service';

@Injectable({
  providedIn: 'root'
})
export class AddToCartService {
  private apiPort = environment.apiPort;
  private BaseUrl: string = `https://localhost:${this.apiPort}/`;


constructor(private httpClient : HttpClient, private headerService: HeaderService) { }

AddRecipeToCart(CartItemData:object):Observable<any>{
  const headers = this.headerService.getHeader();
 return this.httpClient.post(this.BaseUrl+"api/CartItem",CartItemData,{headers});
}

}




