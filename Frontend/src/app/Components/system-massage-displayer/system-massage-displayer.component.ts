import { Component } from '@angular/core';
import {SystemMassageType} from "../../Services/SystemMassages/system-massages.service";
import {NgForOf, NgIf} from "@angular/common";
import {IMessage, ISystemMessageService} from "../../Interfaces/i-system-message-service";
import {ArgumentOutOfRangeError} from "rxjs";

@Component({
  selector: 'app-system-massage-displayer',
  standalone: true,
  imports: [
    NgIf,
    NgForOf
  ],
  templateUrl: './system-massage-displayer.component.html',
  styleUrl: './system-massage-displayer.component.css'
})
export class SystemMassageDisplayerComponent {
  protected messages: IMessage[] = [];
  constructor(private _systemMassageService : ISystemMessageService) {}

  ngOnInit() {
    this._systemMassageService.messages$.subscribe(massages => {
      this.messages = massages;
    });
  }
}
