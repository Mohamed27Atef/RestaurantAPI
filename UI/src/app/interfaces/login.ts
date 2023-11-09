export interface UserLogin {
  email: string;
  password: string;
  passwordVisible?: boolean;
  rememberMe?: boolean;
  emailError?: string;
  passwordError?: string;
  emailOrPasswordError?: string;
}
