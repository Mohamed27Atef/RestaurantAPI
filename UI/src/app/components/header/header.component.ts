//here
import { ShoppingCartService } from 'src/app/services/ShoppingCart.service';
import { Observable, of } from 'rxjs';

import { Block } from '@angular/compiler';
import {
  Component,
  ViewChild,
  ElementRef,
  Input,
  HostListener,
  OnInit,
  AfterViewInit,
} from '@angular/core';
import { Cookies, getCookie, removeCookie } from 'typescript-cookie';
import jwtDecode from 'jwt-decode';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { IsAuthService } from 'src/app/services/is-auth.service';
import { CartItem } from 'src/app/interfaces/CartItem';
import { GetRoleService } from 'src/app/services/get-role.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  totalPrice: number = 0;
  myroute!: string;
  jsonTokenWithoutDecode!: any;
  cartItems$!: Observable<any[]>; // Change this to an Observable
  cartItems: CartItem[] = [];
  role='';
  // isAuth: Boolean = this.isAuthServices.isAuth;
  constructor(
    private cartService: ShoppingCartService,
    public route: Router,
    public isAuthServices: IsAuthService,
    private getRoleService:GetRoleService
  ) {
    this.jsonTokenWithoutDecode = getCookie('User');
    let UserImageFromCookie: any = getCookie('UserImage');
    try {
      let Token: any = jwtDecode(this.jsonTokenWithoutDecode);
      this.name =
        Token != null
          ? Token['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
          : '';
      this.userImage = UserImageFromCookie;
    } catch (error) {
      // console.error('Error decoding JWT:', error);
    }
  }
  isCartVisible: boolean = false;

  @ViewChild('cart') cart!: ElementRef;
  @HostListener('document:click', ['$event'])
  clickOutsideCart(event: Event) {
    if (!this.cart.nativeElement.contains(event.target) && this.isCartVisible) {
      this.isCartVisible = false;
    }
  }

  getCookie(cookie: string){
    this.role=this.getRoleService.GetRoleBytoken(cookie);

  }
  ngOnInit() {
    this.role=this.getRoleService.GetRole();
     }
  updateItems() {
    this.cartService.getCartItems().subscribe({
      next: (items) => {
        this.cartItems = items;
        console.log(this.cartItems);
      },
    });
  }

  toggleCart() {
    this.isCartVisible = !this.isCartVisible;
    if (this.isCartVisible) {
      this.cartService.getCartItems().subscribe((items) => {
        this.cartItems$ = of(items);
        this.updateTotalPrice();
      });
    }
  }
  updateTotalPrice() {
    this.cartItems$.subscribe((items) => {
      this.totalPrice = items.reduce(
        (total, item) => total + item.price * item.quantity,
        0
      );
    });
  }
  navbarCollapsed = true;
  toggalClass = 'navbar-toggler navbar-toggler-right';
  divClass = 'collapse navbar-collapse';

  showToggle() {
    this.navbarCollapsed = !this.navbarCollapsed;
    if (this.navbarCollapsed) {
      this.divClass = 'collapse navbar-collapse';
      this.toggalClass = 'navbar-toggler navbar-toggler-right';
      this.toggalClass = 'navbar-toggler collapsed';
    } else {
      this.toggalClass = 'navbar-toggler collapsed';
      this.divClass = 'collapse navbar-collapse show';
    }
  }
  @ViewChild('closeModal') closeModal!: ElementRef;
  @ViewChild('goToLogin') goToLogin!: ElementRef;

  closeModalClick() {
    this.closeModal.nativeElement.click();
  }
  goToLoginClick() {
    this.goToLogin.nativeElement.click();
  }

  name: string = '';
  userImage: any = null;
  userName(user: any) {
    this.name = user.name;
    this.userImage = user.image;
  }

  toggleLogoutButton(event: Event) {
    if (this.name != '') {
      event.preventDefault();
      const logOutButton = document.getElementById('logOut');
      if (logOutButton) {
        logOutButton.classList.toggle('hidden');
        if (window.innerWidth <= 768) {
          logOutButton.style.top = logOutButton.classList.contains('hidden')
            ? '260px'
            : '275px';
        } else {
          logOutButton.style.top = logOutButton.classList.contains('hidden')
            ? '-40px'
            : '55px';
        }
      }
    }
  }

  LogOut() {
    this.name = '';
    removeCookie('User');
    removeCookie('UserImage');
  }

  clearLink() {
    this.myroute = this.route.url.split('#')[0];
  }

  toggleTable(event: Event) {
    if (this.name != '') {
      event.preventDefault();
      const table = document.getElementById('tables');
      if (table) {
        table.classList.toggle('hidden');
        if (window.innerWidth <= 768) {
          table.style.top = table.classList.contains('hidden')
            ? '260px'
            : '275px';
        } else {
          table.style.top = table.classList.contains('hidden')
            ? '-40px'
            : '55px';
        }
      }
    }
  }
}
