import {ErrorHandler, Injectable, NgZone} from '@angular/core';
import {ISystemMessageService} from "../../Interfaces/i-system-message-service";

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private _systemMassages: ISystemMessageService, private ngZone: NgZone) {}

  handleError(error: any) {
    this.ngZone.run(() => {
      if(error.message.includes('Response closed without headers'))
        this._systemMassages.showError('No server connection');
      else
        this._systemMassages.showError(error.message || 'Undefined error')
    })
    console.error('Error from global error handler', error);
  }
}
