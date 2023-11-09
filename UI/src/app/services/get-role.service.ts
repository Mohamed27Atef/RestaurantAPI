import { Injectable } from '@angular/core';
import { getCookie } from 'typescript-cookie';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class GetRoleService {

  constructor() { }

  GetRole():string{
    let role:string=""
    const helper = new JwtHelperService();
    let Token = getCookie('User');
    console.log(Token);
    if(Token){
    const decodedToken = helper.decodeToken(Token);
    console.log(decodedToken)
    role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    console.log("role is :"+role)
    }
    return role;
  }

  GetRoleBytoken(Token: string):string{
    let role:string=""
    const helper = new JwtHelperService();
    if(Token){
    const decodedToken = helper.decodeToken(Token);
    console.log(decodedToken)
    role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    console.log("role is :"+role)
    }
    return role;
  }
}
