import { Component } from '@angular/core';
import { Table } from 'src/app/interfaces/table';
import { TableService } from 'src/app/services/table.service';

@Component({
  selector: 'app-create-table',
  templateUrl: './create-table.component.html',
  styleUrls: ['./create-table.component.css'],
})
export class CreateTableComponent {
  name: string = '';
  tableObj!: Table;
  constructor(private tableService: TableService) {}

  onSubmit() {
    console.log('Submitted Name: ' + this.name);
    this.tableObj = {
      id: 0,
      tableType: this.name,
    };
    console.log(this.tableObj);

    this.tableService.createTable(this.tableObj).subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (e) => {
        console.log(e);
      },
    });
    this.name = '';
  }
}
