import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getCookie } from 'typescript-cookie';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  constructor() { }

  getHeader() {
    let JsonToken = getCookie('User');
    let Token = JsonToken !=undefined? JSON.parse(JsonToken):null;
     return new HttpHeaders({
       Authorization: `Bearer ${Token}`
     });
  }
}
