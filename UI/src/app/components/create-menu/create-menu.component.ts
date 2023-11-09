import { Component } from '@angular/core';
import { Menu } from 'src/app/interfaces/menu';
import { MenuService } from 'src/app/services/menu.service';
@Component({
  selector: 'app-create-menu',
  templateUrl: './create-menu.component.html',
  styleUrls: ['./create-menu.component.css'],
})
export class CreateMenuComponent {
  name: string = '';
  menuObj!: Menu;

  constructor(private menuService: MenuService) {}
  onSubmit() {
    console.log('Submitted Name: ' + this.name);
    this.menuObj = {
      title: this.name,
      id: 0,
    };
    console.log(this.menuObj);

    this.menuService.createMenu(this.menuObj).subscribe({
      next: (data) => {
        console.log(data);
        this.name = '';
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
}
