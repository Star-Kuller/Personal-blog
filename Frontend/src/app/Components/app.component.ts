import {Component, Inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {NgIf} from "@angular/common";
import {GRPC_INTERCEPTORS, GrpcCoreModule} from '@ngx-grpc/core';
import { GrpcWebClientModule } from '@ngx-grpc/grpc-web-client';


@Component({
  imports: [
    RouterOutlet,
    LoginComponent,
    NgIf,
    GrpcCoreModule,
    GrpcWebClientModule
  ],
  selector: 'app-root',
  standalone: true,
  styleUrl: './app.component.css',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(@Inject(GRPC_INTERCEPTORS) interceptors: any[]) {
    console.log('GRPC_INTERCEPTORS:', interceptors);
  }
}
