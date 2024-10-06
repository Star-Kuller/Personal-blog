import {Component} from '@angular/core';
import {HelloReply, HelloRequest} from "../../../generated/greet_pb";
import {GreeterClient, ServiceError} from "../../../generated/greet_pb_service";
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {grpc} from "@improbable-eng/grpc-web";
import {TOKEN_KEYWORD} from "../../Clients/base-client.service";

@Component({
  selector: 'app-greeter',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    NgForOf,
  ],
  templateUrl: './greeter.component.html',
  styleUrl: './greeter.component.css'
})
export class GreeterComponent {
  client = new GreeterClient('https://localhost:7049');
  inputText: string = '';
  stringArray: string[] = [];

  onSubmit() {
    if (this.inputText.trim()) {
      const client = new GreeterClient('https://localhost:7049');
      const req = new HelloRequest();
      req.setName(this.inputText);
      this.stringArray.push(`Frontend request: ${this.inputText}`);

      let metadata = new grpc.Metadata;
      metadata.headersMap["Authorization"] = [`Bearer ${localStorage.getItem(TOKEN_KEYWORD)}`];

      client.sayHello(req, metadata, (err: ServiceError | null, response: HelloReply | null) => {
        if (err) {
          this.stringArray.push(`Error! Code: ${err.code}  | Massage: ${err.message}`);
          return;
        }
        this.stringArray.push(`Backend response: ${response?.getMessage()}`);
      });
      this.inputText = '';
    }
  }
}
