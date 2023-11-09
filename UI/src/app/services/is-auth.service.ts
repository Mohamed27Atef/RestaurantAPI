import { Injectable } from '@angular/core';
import { getCookie } from 'typescript-cookie';
const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;
@Injectable({
  providedIn: 'root'
})
export class IsAuthService {
  public isAuth: boolean = false;
  constructor() {
    if(!this.isAuth)
      if(getCookie('User'))
      this.isAuth = true;

   }
}
