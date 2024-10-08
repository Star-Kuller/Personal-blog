import { Injectable } from '@angular/core';
import {BaseClientService, TOKEN_KEYWORD} from "../base-client.service";
import {AuthorizationClient} from "../../../generated/auth_pb_service";
import {ServiceError} from "../../../generated/greet_pb_service";
import {LoginForm, RegisterForm, TokenResponse} from "../../../generated/auth_pb";
import {ISystemMessageService} from "../../Interfaces/i-system-message-service";
import {grpc} from "@improbable-eng/grpc-web";
import {RpcError} from "../../Errors/RpcError";

@Injectable({
  providedIn: 'root'
})
export class AuthClientService extends BaseClientService {
  private _client : AuthorizationClient;
  constructor(systemMassages : ISystemMessageService) {
    super(systemMassages);
    this._client = new AuthorizationClient(this.host);
  }

  public login(accountName : string, password : string)
  {
    const req = new LoginForm();
    req.setAccountname(accountName);
    req.setPassword(password);

    this._client?.login(req, (err: ServiceError | null, response?: TokenResponse | null) => {
      if (err) {
        if(err.code == grpc.Code.NotFound){
          this._systemMassages.showInfo(err.message)
          return;
        }
        throw new RpcError(err.message, err.code, err.metadata)
      }
      if(response?.getToken())
        localStorage.setItem(TOKEN_KEYWORD, response?.getToken());
    });
  }

  public register(name : string, accountName : string, password : string)
  {
    const req = new RegisterForm();
    req.setName(name);
    req.setAccountname(accountName);
    req.setPassword(password);

    this._client?.register(req, (err: ServiceError | null, response?: TokenResponse | null) => {
      if (err) {
        if(err.code == grpc.Code.AlreadyExists){
          this._systemMassages.showInfo(err.message)
          return;
        }
        throw new RpcError(err.message, err.code, err.metadata)
      }
      if(response?.getToken())
        localStorage.setItem('token', response?.getToken());
    });
  }
}
