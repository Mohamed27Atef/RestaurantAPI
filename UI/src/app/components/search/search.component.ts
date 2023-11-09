import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/interfaces/category';
import { Restaurant } from 'src/app/interfaces/restaurant';
import { CategoryService } from 'src/app/services/category.service';
import { LocatinService } from 'src/app/services/locatin.service';
import { RestaurantService } from 'src/app/services/restaurant.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  category_id: number = 0;
  categories!: Category[];
  isSearch: boolean = false;
  restaurantResult: Restaurant[] = [];
  @Output() myEvent = new EventEmitter();
  @Output() selectedLocatoin = new EventEmitter();
  locatoins!: string[];

  constructor(private categoryService : CategoryService, private RestaurantService: RestaurantService, private locatoinServices: LocatinService){

  }

  ngOnInit(): void {
    this.categoryService.getAllCategory().subscribe({
      next: data => {
        this.categories = data;
      },
      error: err => console.log(err)
    })
  }


  SelectCategory(categorySelected : string) {
    this.category_id = Number(categorySelected);
    this.myEvent.emit(categorySelected);
  }

  searchOfRestaurantByName(name : string) {
    this.isSearch = true
    if(name == ""){
      this.getByCategoryId();
      this.isSearch = false;
    }
    else
      this.searchRestaurantBycategoryAndName(name);
  }

  getByCategoryId() {
    this.RestaurantService.getRestaurantByCategoryId(this.category_id).subscribe({
      next : data => this.restaurantResult = data,
    })
  }
  searchRestaurantBycategoryAndName(name: string) {
    this.RestaurantService.searchRestaurantByNameAndCategory(name, this.category_id).subscribe({
      next: data => {
        this.restaurantResult = data;
      },
      error: err => console.log(err)
    })
  }

  

}
