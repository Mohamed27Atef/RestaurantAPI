import { Component } from '@angular/core';

@Component({
  selector: 'app-get-location',
  templateUrl: './get-location.component.html',
  styleUrls: ['./get-location.component.css']
})
export class GetLocationComponent {
  Longitude:any
  Latitude:any
  GetCurrentLocation(){
    navigator.geolocation.getCurrentPosition((position)=>{
      this.Longitude=position.coords.longitude
      this.Latitude=position.coords.latitude
      console.log(this.Longitude)
      console.log(this.Latitude)
    })
  }
}
