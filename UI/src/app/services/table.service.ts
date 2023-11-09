import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.dev';
import { getCookie } from 'typescript-cookie';
import { Table } from '../interfaces/table';

const JsonToken = getCookie('User');
const token = JsonToken != undefined ? JSON.parse(JsonToken) : null;

const headers = new HttpHeaders({
  Authorization: `Bearer ${token}`,
});

@Injectable({
  providedIn: 'root',
})
export class TableService {
  private apiPort = environment.apiPort;
  private BaseUrl: string = `https://localhost:${this.apiPort}/`;

  constructor(private httpClient: HttpClient) {}

  createTable(table: Table): Observable<any> {
    return this.httpClient.post(this.BaseUrl + 'api/Table/createTable', table, {
      headers: headers,
    });
  }

  getTableByRestaurantIdAndDAteTime(
    dateTime: string,
    RestaurantId: number
  ): Observable<any> {
    return this.httpClient.get(
      this.BaseUrl +
        'api/Table/' +
        'getAvailableTalbe?time=' +
        dateTime +
        '&restaurantId=' +
        RestaurantId
    );
  }

  reserveTable(reservationData: object): Observable<any> {
    console.log(reservationData);

    return this.httpClient.post(this.BaseUrl + 'api/Table', reservationData, {
      headers,
    });
  }

  getTableType(tableType: string): string {
    switch (tableType) {
      case 'Solo':
        return 'Solo for 1 person';
      case 'Mini':
        return 'Mini for (2-3) persons';
      case 'Medium':
        return 'Medium for (4-5) persons';
      case 'Family':
        return 'Family for +5 persons';
      default:
        return '';
    }
  }
}
