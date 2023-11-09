import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
  items: string[] = [];  //List of items to show
  itemsPerPage = 5; //This how many items per page to show
  currentPage = 1;
  pages: number[] = [];
  pagesToShow = 4;  //how many number between > <
  @Output() changeDataEvent = new EventEmitter<number>()

  constructor() {}

  ngOnInit(): void {
    this.items = this.generateDummyData();
    this.calculatePageNumbers();
  }

  calculatePageNumbers() {
    const startPage = Math.max(1, this.currentPage - Math.floor(this.pagesToShow / 2));
    const endPage = Math.min(this.totalPages, startPage + this.pagesToShow - 1);

    this.pages = Array(endPage - startPage + 1).fill(0).map((_, i) => startPage + i);
  }

  get totalPages(): number {
    return Math.ceil(this.items.length / this.itemsPerPage);
  }

  get itemsOnCurrentPage(): string[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    return this.items.slice(startIndex, endIndex);
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.calculatePageNumbers();
      this.changeDataEvent.emit(this.currentPage);
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.calculatePageNumbers();
      this.changeDataEvent.emit(this.currentPage);
    }
  }

  goToPage(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.totalPages) {
      this.currentPage = pageNumber;
      this.calculatePageNumbers();
      this.changeDataEvent.emit(pageNumber);

    }
  }

  generateDummyData(): string[] {
    const dummyData: string[] = [];
    for (let i = 1; i <= 100; i++) {
      dummyData.push(`Item ${i}`);
    }
    return dummyData;
  }
}



