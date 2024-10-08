import {ApplicationConfig, ErrorHandler} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {ISystemMessageService} from "./Interfaces/i-system-message-service";
import {SystemMassageService} from "./Services/SystemMassages/system-massages.service";
import {GlobalErrorHandler} from "./Services/SystemMassages/global-error-handler.service";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    { provide: ISystemMessageService, useClass: SystemMassageService },
    { provide: ErrorHandler, useClass: GlobalErrorHandler }
  ]
};
