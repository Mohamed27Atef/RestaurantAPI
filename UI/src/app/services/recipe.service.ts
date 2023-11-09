import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { Observable } from 'rxjs';
import { Recipe } from '../interfaces/recipe';
import { getCookie } from 'typescript-cookie';
import { HeaderService } from './header.service';
import {MostPopularRecipe} from'../interfaces/MostPopularRecipe';
const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});
@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  constructor(private myClient: HttpClient, private header: HeaderService) {}
  private apiPort = environment.apiPort;
  private DB_URL = `https://localhost:${this.apiPort}/api/Recipe/`;

  createRecipe(recipe: Recipe) {
    const headers = this.header.getHeader();
    return this.myClient.post(this.DB_URL, recipe, {headers});
  }

  getRecipe(id: number): Observable<any> {
    return this.myClient.get(this.DB_URL + id);
  }

  getRecipeByMenuId(id: number) {
    return this.myClient.get(this.DB_URL + 'getRecipeByMenuId/' + id);
  }
  //This to load all recipes in Recipe Filter page
  getRecipes(): Observable<Recipe[]> {
    return this.myClient.get<Recipe[]>(this.DB_URL);
  }

  searchRecipesByName(name: string, page: number = 1) {
    const url = `${this.DB_URL}search/${name}?p=${page}`;
    return this.myClient.get<Recipe[]>(url);
  }
  searchRecipeInResByName(name: string, page: number = 1) {
    const url = `${this.DB_URL}searchReceipeInResturant/${name}?p=${page}`;
    return this.myClient.get<Recipe[]>(url, { headers: headers });
  }

  getMostPopularRecipes(): Observable<MostPopularRecipe[]> {
    const url = `${this.DB_URL}getMostRated`; 
    return this.myClient.get<MostPopularRecipe[]>(url);
  }

}
