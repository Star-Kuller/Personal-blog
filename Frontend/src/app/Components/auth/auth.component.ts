import { Component } from '@angular/core';
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {AuthClientService} from "../../Clients/login/auth-client.service";

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    NgIf,
    FormsModule
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  constructor(private client : AuthClientService) {
  }

  isLogin = true;
  displayName = '';
  accountName = '';
  password = '';
  passwordRepeat = '';
  onSubmit() {
    if(this.isLogin)
      this.login()
    else
      this.register()
  }

  login() {
    this.client.login(this.accountName, this.password)
  }

  register() {
  }
}
