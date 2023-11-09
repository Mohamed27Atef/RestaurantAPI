import { Component, EventEmitter, Output } from '@angular/core';
import { SignUpService } from 'src/app/services/sign-up.service';
import { User } from '../../interfaces/signup.interface';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  constructor(private myService: SignUpService) {}
  user: User = {
    firstName: '',
    LastName: '',
    email: null,
    password: '',
    confirmPassword: '',
  };

  @Output() clickEvent = new EventEmitter<void>();
  registerCheck: boolean = false;
  checkboxError: boolean = false;
  togglePasswordVisibility() {
    this.user.passwordVisible = !this.user.passwordVisible;
  }
  togglePasswordVisibility2() {
    this.user.confirmPasswordVisible = !this.user.confirmPasswordVisible;
  }
  signUp() {
    let signUpData: any = {
      firstName: this.user.firstName,
      lastName: this.user.LastName,
      email: this.user.email,
      password: this.user.password,
      confirmPassword: this.user.confirmPassword,
      phone: '63010714182',
      address: 'string',
    };
    console.log(signUpData)
    if (!this.registerCheck) {
      this.checkboxError = true; // Apply the red border
    } else {
      this.checkboxError = false; // Remove the red border
    }
    this.myService.register(signUpData).subscribe({
      next: () => {
        console.log('Success Sign Up');
        this.clickEvent.emit();
      },
      error: (errorMassage) => {
        console.log(errorMassage);
        this.user.firstNameError =
          this.user.firstName == ''
            ? 'First Name is required.'
            : errorMassage.error.errors.FirstName;
        this.user.LastNameError =
          this.user.LastName == ''
            ? 'Last Name is required.'
            : errorMassage.error.errors.LastName;
        this.user.emailError =
          this.user.email == null
            ? 'Email is required.'
            : errorMassage.error.errors.Email[0];
        this.user.passwordError =
          this.user.password == ''
            ? 'Password is required.'
            : errorMassage.error.errors.Password;
        this.user.confirmPasswordError =
          this.user.confirmPassword == ''
            ? 'Confirm Password is required.'
            : errorMassage.error.errors.ConfirmPassword;
        console.log(errorMassage.error.errors.Email[0]);
      },
    });
  }
}
