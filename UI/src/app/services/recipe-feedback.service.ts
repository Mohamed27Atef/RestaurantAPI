import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.dev';
import { Observable } from 'rxjs';
import { getCookie } from 'typescript-cookie';

@Injectable({
  providedIn: 'root',
})
export class RecipeFeedbackService {
  private apiPort = environment.apiPort;
  private apiUrl = `https://localhost:${this.apiPort}/api/RecipeFeedback`;

  constructor(private http: HttpClient) {}

  postFeedback(feedbackData: any) {
    const JsonToken = getCookie('User');
    const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.post(`${this.apiUrl}`, feedbackData, { headers });
  }

  getReviewsForRecipe(recipeId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/recipe/${recipeId}`);
  }

  getNumberOfReviews(recipeId: number): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/getNumberOfReview/${recipeId}`);
  }
}
