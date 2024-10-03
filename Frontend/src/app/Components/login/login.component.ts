import { Component } from '@angular/core';
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    NgIf,
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  isLogin = true;
  username = '';
  password = '';
  passwordRepeat = '';
  onSubmit() {
    if(this.isLogin)
      this.login()
    else
      this.register()
  }

  login() {

  }

  register() {
  }
}
