import { Component, Input } from '@angular/core';

@Component({
  selector: 'restaurant-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  @Input() address!: string;
  @Input() phone!: string;
  @Input() email!: string;

}
