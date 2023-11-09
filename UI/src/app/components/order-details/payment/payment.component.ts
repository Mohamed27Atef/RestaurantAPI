import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { OrderDetails } from 'src/app/interfaces/order-details';
import { OrderdAddress } from 'src/app/interfaces/orderd-address';
import { CoponService } from 'src/app/services/copon.service';
import { CreateorderService } from 'src/app/services/createorder.service';
import { OrderDetailsService } from 'src/app/services/order-details.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css'],
})
export class PaymentComponent {
  street: string = '';
  city: string = '';
  country: string = '';
  oneCopon: boolean = true;
  oldTotalPrice = 0;
  orderAddress: OrderdAddress = {
    street: '123 Main St',
    city: 'Example City',
    country: 'Example Country',
    TotalPrice: 100.0,
  };
  coponObj: any;
  showInput: boolean = false;
  promoCode: string = '';

  CartItemSumary!: OrderDetails[];

  RegisterationValidation = new FormGroup({
    street: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(30),
    ]),
    city: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(30),
    ]),
    country: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(30),
    ]),
  });

  constructor(
    private orderdetailService: OrderDetailsService,
    private createorder: CreateorderService,
    private route: Router,
    private myCopon: CoponService
  ) {
    this.orderdetailService.getAllCartItems().subscribe({
      next: (data) => {
        console.log(data);
        this.CartItemSumary = data;
        console.log(this.CartItemSumary);
        this.calculateTotalPrice();
      },
      error: (err) => console.log(err),
    });
  }

  get StreetValid() {
    return this.RegisterationValidation.controls['street'].valid;
  }
  get cityValid() {
    return this.RegisterationValidation.controls['city'].valid;
  }
  get countryValid() {
    return this.RegisterationValidation.controls['country'].valid;
  }

  togglePromoInput() {
    this.showInput = !this.showInput;
  }
  applyPromoCode() {
    this.myCopon.getCoponByName(this.promoCode).subscribe({
      next: (data) => {
        this.coponObj = data;
        console.log(this.coponObj);
        if (this.coponObj.discountPercentage != -1 && this.oneCopon) {
          this.oldTotalPrice = this.orderAddress.TotalPrice;
          this.oneCopon = false;
          this.orderAddress.TotalPrice -=
            this.CartItemSumary.reduce(
              (total, item) => total + item.totalPrice,
              0
            ) *
            (this.coponObj.discountPercentage / 100);
        }
      },
      error: (err) => console.log(err),
    });
  }
  calculateTotalPrice() {
    this.orderAddress.TotalPrice = this.CartItemSumary.reduce(
      (total, item) => total + item.totalPrice,
      0
    );
  }

  makeOrder() {
    this.orderAddress.street = this.street;
    this.orderAddress.city = this.city;
    this.orderAddress.country = this.country;
    this.createorder.postCartItem(this.orderAddress).subscribe({
      next: (data) => {
        this.route.navigate(["/orders"]);
      },
      error: (err) => this.route.navigate(["/orders"])
    });
  }

  onSubmit() {
    // if (this.street == '' || this.city == '' || this.country == '') {
    // }
  }
}
