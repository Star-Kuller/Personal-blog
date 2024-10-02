import { Component } from '@angular/core';
import {Router, RouterOutlet} from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginComponent, NgIf],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
}
