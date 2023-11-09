import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements AfterViewInit {


  @Input() latitude!: number
  @Input() longitude!: number

  @ViewChild('map') map!: ElementRef;

  ngAfterViewInit(): void {
    this.map.nativeElement.src = this.map.nativeElement.src.replace('!3d21.528995880248495', '!3d' + this.latitude);
    this.map.nativeElement.src = this.map.nativeElement.src.replace('!2d39.18602027527065', '!2d' + this.longitude);
  
  }

}
