import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { Restaurant } from '../interfaces/restaurant';
import { getCookie } from 'typescript-cookie';

const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  private apiPort = environment.apiPort;
  private BaseUrl: string = `https://localhost:${this.apiPort}/api/Resturant`;

  constructor(private httpClient: HttpClient) {}

  createResturant(resturant: Restaurant) {
    return this.httpClient.post(this.BaseUrl, resturant);
  }

  getRestaurantByِApplicationId(): Observable<any> {
    return this.httpClient.get(this.BaseUrl + '/getByAppID', { headers });
  }
  getRestaurantByCategoryId(categoryId: number): Observable<any> {
    return this.httpClient.get(this.BaseUrl + '/getByCategory/' + categoryId);
  }

  searchRestaurantByNameAndCategory(
    name: string,
    categoryId: number
  ): Observable<any> {
    return this.httpClient.get(
      this.BaseUrl + '/search/?q=' + name + '&cat=' + categoryId
    );
  }

  getAllRestaurant(): Observable<any> {
    return this.httpClient.get(this.BaseUrl);
  }
  getRestaurantByName(q: string): Observable<any> {
    return this.httpClient.get(this.BaseUrl + '/search/' + q);
  }

  getRestaurantById(id: number): Observable<any> {
    return this.httpClient.get(this.BaseUrl + '/' + id);
  }

  getRestaurantByLocation(location: string): Observable<any> {
    return this.httpClient.get(this.BaseUrl + '/getByAddress/' + location);
  }

  getRestaurantImages(id: number): Observable<any> {
    return this.httpClient.get(this.BaseUrl + '/getimages/' + id);
  }
}
