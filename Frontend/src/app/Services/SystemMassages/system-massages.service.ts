import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {IMessage, ISystemMessageService} from "../../Interfaces/i-system-message-service";

export enum SystemMassageType {
  error = 'error',
  warn = 'warn',
  info = 'info'
}

@Injectable({
  providedIn: 'root',
})

export class SystemMassageService extends ISystemMessageService{
  get messages$(): Observable<IMessage[]> {
    return this._massages$;
  }

  private _subject = new BehaviorSubject<IMessage[]>([]);
  private _massages$ = this._subject.asObservable();
  private _counter = 0;

  showError(message: string, milliseconds: number = 2500) {
    return this.show(message, milliseconds, SystemMassageType.error)
  }

  showWarn(message: string, milliseconds: number = 2500) {
    return this.show(message, milliseconds, SystemMassageType.warn)
  }

  showInfo(message: string, milliseconds: number = 2500) {
    return this.show(message, milliseconds, SystemMassageType.info)
  }

  private show(message: string, milliseconds: number, type: SystemMassageType){
    const newMassage: IMessage = {
      id: this._counter++,
      text: message,
      type: type
    };
    const current = this._subject.value;
    this._subject.next([...current, newMassage]);

    setTimeout(() => this.removeMassage(newMassage.id), milliseconds);
    return newMassage.id;
  }

  public removeMassage(id: number) {
    const currentErrors = this._subject.value;
    this._subject.next(currentErrors.filter((error) => error.id !== id));
  }
}
