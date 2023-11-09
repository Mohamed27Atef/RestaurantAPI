import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Copon } from 'src/app/interfaces/copon';
import { CoponService } from 'src/app/services/copon.service';

@Component({
  selector: 'app-create-copon',
  templateUrl: './create-copon.component.html',
  styleUrls: ['./create-copon.component.css'],
})
export class CreateCoponComponent {
  form: FormGroup;
  coponObj!: Copon;

  constructor(private myCopon: CoponService, private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      NumberOfUser: [null, Validators.required],
      Text: ['', Validators.required],
      DiscountPercentage: [null, [Validators.min(0), Validators.max(100)]],
    });
  }
  onSubmit() {
    if (this.form.valid) {
      // Handle form submission here, for example, send data to the server
      const formData = this.form.value;
      console.log(formData);

      this.coponObj = {
        NumberOfUser: +formData.NumberOfUser,
        Text: formData.Text,
        DiscountPercentage: +formData.DiscountPercentage,
      };
      this.myCopon.createCopon(this.coponObj).subscribe({
        next: (data) => {
          console.log(data);
          this.form.reset();
        },
        error: (e) => {
          console.log(e);
        },
      });
    }
  }
}
