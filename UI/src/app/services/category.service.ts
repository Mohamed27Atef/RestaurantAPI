import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private apiPort = environment.apiPort;
  private BaseUrl: string = `https://localhost:${this.apiPort}/api/Category`;

  constructor(private htppClient: HttpClient) {}

  getAllCategory(): Observable<any> {
    return this.htppClient.get(this.BaseUrl);
  }
}
