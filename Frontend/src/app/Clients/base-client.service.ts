import {grpc} from "@improbable-eng/grpc-web";
import {environment} from "../../environments/environment";
import {ISystemMessageService} from "../Interfaces/i-system-message-service";

export const TOKEN_KEYWORD = 'token';

export class BaseClientService {

  host : string = environment.host;

  constructor( protected _systemMassages : ISystemMessageService) {}

  protected get defaultMetadata() : grpc.Metadata {
    let metadata = new grpc.Metadata;
    metadata.headersMap["Authorization"] = [`Bearer ${localStorage.getItem(TOKEN_KEYWORD)}`];
    return metadata;
  }
}
