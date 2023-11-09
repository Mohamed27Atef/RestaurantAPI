import { Component,Input,OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { StepsService } from 'src/app/services/steps.service';

@Component({
  selector: 'app-steps',
  templateUrl: './steps.component.html',
  styleUrls: ['./steps.component.css'],
  animations: [
    trigger('lineAnimation', [
      state('processed', style({ width: '0%', marginLeft: '10%' })),
      state('shipped', style({ width: '40%', marginLeft: '13%' })),
      state('enRoute', style({ width: '44%', marginLeft: '13%' })),
      state('arrived', style({ width: '67%', marginLeft: '13%' })),
      transition('* => *', animate('500ms ease-in-out')),
    ]),
  ],
})
export class StepsComponent implements OnInit {
  @Input() status!: string
  constructor(private StepsServices: StepsService) {}

  getStepStatus(stepNumber: number): string {
    if (this.status === 'processed' && stepNumber <= 1) {
      return 'active';
    } else if (this.status === 'shipped' && stepNumber <= 2) {
      return 'active';
    } else if (this.status === 'enRoute' && stepNumber <= 3) {
      return 'active';
    } else if (this.status === 'arrived' && stepNumber <= 4) {
      return 'active';
    } else {
      return 'inactive';
    }
  }
  ngOnInit() {
    this.StepsServices.getStatus().subscribe((data: any) => {
      this.status = data.status;
    });
  }
}
