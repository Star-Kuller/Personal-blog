import {ErrorHandler, Injectable, NgZone} from '@angular/core';
import {ISystemMessageService} from "../../Interfaces/i-system-message-service";
import {RpcError} from "../../Errors/RpcError";
import {grpc} from "@improbable-eng/grpc-web";
import Code = grpc.Code;
import {TOKEN_KEYWORD} from "../../Clients/base-client.service";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private _systemMassages: ISystemMessageService, private ngZone: NgZone) {}

  handleError(error: any) {
    this.ngZone.run(() => {
      if(error instanceof RpcError)
        this.handleRpcErrors(error);
      else
        this._systemMassages.showError(error.message || 'Undefined error');
    })
    console.error('Error from global error handler', error);
  }

  private handleRpcErrors(error: RpcError){
    switch (error.code){
      case Code.FailedPrecondition:
      case Code.Unavailable:
      case Code.OutOfRange:
      case Code.NotFound:
      case Code.DataLoss:
      case Code.Unknown:
      case Code.Aborted:
        this._systemMassages.showError(error.message);
        break;
      case Code.ResourceExhausted:
      case Code.DeadlineExceeded:
      case Code.InvalidArgument:
      case Code.AlreadyExists:
        this._systemMassages.showWarn(error.message)
        break;
      case Code.PermissionDenied:
        this._systemMassages.showWarn('No server connection');
        break;
      case Code.Unauthenticated:
        this._systemMassages.showWarn('You need to log in');
        localStorage.removeItem(TOKEN_KEYWORD);
        break;
      case Code.Unimplemented:
        this._systemMassages.showWarn('Oops... This feature is not available now');
        break;
      case Code.Canceled:
        this._systemMassages.showError('The request was been cancelled');
        break;
      case Code.Internal:
        this._systemMassages.showError('Oops... There was a trouble with the servers');
        break;
      case Code.OK:
        this._systemMassages.showInfo(error.message)
        break;
      default:
        console.log(`Undefined rpc error: { code:${error.code} | massage: ${error.message} | headers: ${error.metadata.toHeaders()} }`)
        this._systemMassages.showError('Undefined connection error');
    }
  }
}
