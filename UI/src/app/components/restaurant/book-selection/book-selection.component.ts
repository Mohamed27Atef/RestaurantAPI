import { Component,Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-book-selection',
  templateUrl: './book-selection.component.html',
  styleUrls: ['./book-selection.component.css']
})
export class BookSelectionComponent {
  @Output() changeDataEvent = new EventEmitter();
 
 
  performAction() {
    this.changeDataEvent.emit();
  }
}   