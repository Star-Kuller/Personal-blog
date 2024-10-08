import {SystemMassageType} from "../Services/SystemMassages/system-massages.service";
import {BehaviorSubject, Observable} from "rxjs";

export interface IMessage {
  id: number;
  text: string;
  type: SystemMassageType;
}

export abstract class ISystemMessageService {
  abstract get messages$() : Observable<IMessage[]>
  abstract showError(message: string, milliseconds?: number) : number
  abstract showWarn(message: string, milliseconds?: number) : number
  abstract showInfo(message: string, milliseconds?: number) : number
  abstract removeMassage(id: number) : void
}
