import { Component } from '@angular/core';
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {AuthClientService} from "../../Clients/login/auth-client.service";
import {ISystemMessageService} from "../../Interfaces/i-system-message-service";

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
  constructor(private _client : AuthClientService, private _systemMessages: ISystemMessageService) {
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
    this._client.login(this.accountName, this.password);
  }

  register() {
    if(this.password !== this.passwordRepeat){
      this._systemMessages.showWarn("Password and repeat password must be identical");
    }
    this._client.register(this.displayName, this.accountName, this.password);
  }
}
