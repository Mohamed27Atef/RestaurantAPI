import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class SignUpService {
  constructor(private myClient: HttpClient) {}
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/Account/register`;

  register(registerData: any) {
    return this.myClient.post(this.DB_URL, registerData);
  }
}
