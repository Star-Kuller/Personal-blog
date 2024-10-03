import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NgIf} from "@angular/common";
import {AuthComponent} from "./auth/auth.component";


@Component({
  imports: [
    RouterOutlet,
    AuthComponent,
    NgIf,
  ],
  selector: 'app-root',
  standalone: true,
  styleUrl: './app.component.css',
  templateUrl: './app.component.html'
})
export class AppComponent {
}
