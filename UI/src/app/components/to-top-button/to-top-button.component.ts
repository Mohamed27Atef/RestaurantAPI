import { Component, HostListener, ElementRef } from '@angular/core';

@Component({
  selector: 'app-to-top-button',
  templateUrl: './to-top-button.component.html',
  styleUrls: ['./to-top-button.component.css']
})
export class ToTopButtonComponent {
  toTopBtnVisible: boolean = false;

  constructor(private elementRef: ElementRef) {}

  @HostListener('window:scroll')
  onWindowScroll() {
    const toTopBtn = this.elementRef.nativeElement.querySelector('#toTop');
    if (window.scrollY >= 500) {
      toTopBtn.style.display = 'block';
    } else {
      toTopBtn.style.display = 'none';
    }
  }

  toTop(): void {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }
}