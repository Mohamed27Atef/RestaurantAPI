import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';
import { Copon } from '../interfaces/copon';
import { Observable } from 'rxjs/internal/Observable';
const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});

@Injectable({
  providedIn: 'root',
})
export class CoponService {
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/Copon`;
  constructor(private myClient: HttpClient) {}

  createCopon(copon: Copon): Observable<any> {
    return this.myClient.post(this.DB_URL, copon, {
      headers: headers,
    });
  }

  getCoponByName(name: string): Observable<any> {
    return this.myClient.get(this.DB_URL + '?name=' + name);
  }
}
