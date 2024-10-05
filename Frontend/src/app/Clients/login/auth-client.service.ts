import { Injectable } from '@angular/core';
import { BaseClientService } from "../base-client.service";
import {AuthorizationClient} from "../../../generated/auth_pb_service";
import {ServiceError} from "../../../generated/greet_pb_service";
import {LoginForm, TokenResponse} from "../../../generated/auth_pb";
import {grpc} from "@improbable-eng/grpc-web";

@Injectable({
  providedIn: 'root'
})
export class AuthClientService extends BaseClientService {
  private client : AuthorizationClient;
  constructor() {
    super();
    this.client = new AuthorizationClient(this.host);
  }

  public login(username : string, password : string)
  {
    const req = new LoginForm();
    req.setUsername(username);
    req.setPassword(password);

    this.client?.login(req, (err: ServiceError | null, response?: TokenResponse | null) => {
      if (err) {
        console.log(err.message)
        console.log(err.code)
        console.log(err.metadata)
        return;
      }
      BaseClientService.setJwtToken(response?.getToken() ?? "")
      console.log(`Token: ${response?.getToken()}`)
    });
  }

  public register(username : string, password : string)
  {

  }
}
