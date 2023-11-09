import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ShoppingCartService } from './services/ShoppingCart.service';
import { GetRoleService } from './services/get-role.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  scrollThreshold = 100;
  title = 'restaurant_project';
  role='';
  constructor(private cartService: ShoppingCartService,
    private http: HttpClient,
    private getRoleService:GetRoleService) {}
 
  ngOnInit() {

 this.role=this.getRoleService.GetRole();
  }

  showStatusComponent: boolean = false;

  // toggleStatusComponent() {
  //   this.showStatusComponent = !this.showStatusComponent;
  //   setTimeout(() => {
  //     this.showStatusComponent = !this.showStatusComponent;
  //   }, 2000);
  // }


  scrollToTop(): void {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }
  

}
