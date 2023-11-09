import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root'
})
export class LocatinService {

  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/location`;
  
  constructor(private httpClient: HttpClient) { }

  getAllLocation(): Observable<any> {
    return this.httpClient.get(this.DB_URL);
  }
}
