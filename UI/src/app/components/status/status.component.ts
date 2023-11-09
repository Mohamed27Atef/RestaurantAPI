import { Component, Input, Output, EventEmitter } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css'],
  animations: [
    trigger('slideFromRight', [
      state('*', style({ transform: 'translateX(0)' })),
      state('void', style({ transform: 'translateX(200%)' })),
      transition('void => *', animate('500ms ease-out')),
      transition('* => void', animate('500ms ease-in')),
    ]),
  ],
})
export class StatusComponent {
  @Input() showStatus!: boolean
  constructor(){
    console.log('lfkjasdlkfjasdlkfasdjfas;iofjasdo')
  }
}
