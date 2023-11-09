import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';

const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});
@Injectable({
  providedIn: 'root',
})
export class UserTableServicesService {
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/UserTable`;
  constructor(private myClient: HttpClient) {}

  getAllUserReservation(): Observable<any> {
    return this.myClient.get(`${this.DB_URL}/searchByUserId`, { headers });
  }
  deleteUserReservation(id: number) {
    return this.myClient.delete(`${this.DB_URL}?id=${id}`, { headers });
  }
}
