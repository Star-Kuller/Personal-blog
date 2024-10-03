import {APP_INITIALIZER, ApplicationConfig, InjectionToken} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {GrpcInterceptorHandler} from "./Clients/grpc-interceptor.service";
import {GRPC_INTERCEPTORS} from "@ngx-grpc/core";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    { provide: GRPC_INTERCEPTORS, useClass: GrpcInterceptorHandler, multi: true },
  ]
};
