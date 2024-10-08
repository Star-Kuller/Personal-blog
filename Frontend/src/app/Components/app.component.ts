import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NgIf} from "@angular/common";
import {AuthComponent} from "./auth/auth.component";
import {TOKEN_KEYWORD} from "../Clients/base-client.service";
import {SystemMassageDisplayerComponent} from "./system-massage-displayer/system-massage-displayer.component";


@Component({
  imports: [
    RouterOutlet,
    AuthComponent,
    NgIf,
    SystemMassageDisplayerComponent,
  ],
  selector: 'app-root',
  standalone: true,
  styleUrl: './app.component.css',
  templateUrl: './app.component.html'
})
export class AppComponent {
  protected get isAuthorized() : boolean {
    return !!localStorage.getItem(TOKEN_KEYWORD);
  }
}
