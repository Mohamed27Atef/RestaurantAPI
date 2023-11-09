import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { HeaderService } from './header.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private apiPort = environment.apiPort;

  private DB_URL = `https://localhost:${this.apiPort}/api/Account`;
  constructor(private http: HttpClient, private header: HeaderService) { }

  getUserInfo() : Observable<any>{
    const headers = this.header.getHeader();
    return this.http.get(this.DB_URL+ "/get-profile", {headers})
  } 


}
