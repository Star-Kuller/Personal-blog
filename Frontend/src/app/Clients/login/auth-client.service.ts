import { Injectable } from '@angular/core';
import {BaseClientService, TOKEN_KEYWORD} from "../base-client.service";
import {AuthorizationClient} from "../../../generated/auth_pb_service";
import {ServiceError} from "../../../generated/greet_pb_service";
import {LoginForm, RegisterForm, TokenResponse} from "../../../generated/auth_pb";

@Injectable({
  providedIn: 'root'
})
export class AuthClientService extends BaseClientService {
  private _client : AuthorizationClient;
  constructor() {
    super();
    this._client = new AuthorizationClient(this.host);
  }

  public login(accountName : string, password : string)
  {
    const req = new LoginForm();
    req.setAccountname(accountName);
    req.setPassword(password);

    this._client?.login(req, (err: ServiceError | null, response?: TokenResponse | null) => {
      if (err) {
        console.log(err.message)
        console.log(err.code)
        console.log(err.metadata)
        return;
      }
      if(response?.getToken())
        localStorage.setItem(TOKEN_KEYWORD, response?.getToken());

      console.log(`Token: ${response?.getToken()}`)
    });
  }

  public register(name : string, accountName : string, password : string)
  {
    const req = new RegisterForm();
    req.setName(name);
    req.setAccountname(accountName);
    req.setPassword(password);

    this._client?.login(req, (err: ServiceError | null, response?: TokenResponse | null) => {
      if (err) {
        console.log(err.message)
        console.log(err.code)
        console.log(err.metadata)
        return;
      }
      if(response?.getToken())
        localStorage.setItem('token', response?.getToken());

      console.log(`Token: ${response?.getToken()}`)
    });
  }
}
