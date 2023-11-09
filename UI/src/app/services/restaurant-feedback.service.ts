import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment.dev';
import { Observable } from 'rxjs';
import { IRestaurantFeedback } from 'src/app/interfaces/RestaurantFeedback'
import { getCookie } from 'typescript-cookie';


@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  private apiPort = environment.apiPort;
  private apiUrl = `https://localhost:${this.apiPort}/api/ResturantFeedback`; 
  constructor(private http: HttpClient) {}
  

  
  postFeedback(feedbackData: any) {
    const JsonToken = getCookie('User');
    const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.post(`${this.apiUrl}`, feedbackData, {headers});
  }
  getReviewsForRestaurant(restaurantId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/restaurant/${restaurantId}`);
  }
}
