import {grpc} from "@improbable-eng/grpc-web";

export class BaseClientService {
  protected host = 'https://localhost:7049';
  static defaultMetadata = new grpc.Metadata();

  public static setJwtToken( token : string ){
    this.defaultMetadata.headersMap["Authorization"] = [`Bearer ${token}`];
  }
}
