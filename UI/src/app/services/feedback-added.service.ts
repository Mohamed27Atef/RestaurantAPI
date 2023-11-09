import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';
@Injectable({
  providedIn: 'root'
})
export class FeedbackAddedService {
  private apiPort = environment.apiPort;
  private BaseUrl: string = `https://localhost:${this.apiPort}/`;

  constructor(private httpClient : HttpClient) { }

  checkIfFeedbackAddedToRecipe(RecipeId:number):Observable<any>{
    console.log(RecipeId)
    let JsonToken = getCookie('User');
    console.log(JsonToken)
   let Token = JsonToken !=undefined? JSON.parse(JsonToken):null;
    console.log(Token)

    const headers = new HttpHeaders({
      Authorization: `Bearer ${Token}`
    });
    return this.httpClient.get(this.BaseUrl+"api/RecipeFeedback/IsFeedbackAddedToRecipe/"+RecipeId,{headers});
  }

  checkIfFeedbackAddedToRestaurant(RestaurantId:number):Observable<any>{
    console.log(RestaurantId)
    let JsonToken = getCookie('User');
    console.log(JsonToken)
   let Token = JsonToken !=undefined? JSON.parse(JsonToken):null;
    console.log(Token)

    const headers = new HttpHeaders({
      Authorization: `Bearer ${Token}`
    });
    return this.httpClient.get(this.BaseUrl+"api/ResturantFeedback/IsFeedbackAddedToRestaurant/"+RestaurantId,{headers});
  }
}
 