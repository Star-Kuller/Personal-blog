import {grpc} from "@improbable-eng/grpc-web";

export const TOKEN_KEYWORD = 'token';

export class BaseClientService {
  protected host = 'https://localhost:7049';

  protected get defaultMetadata() : grpc.Metadata {
    let metadata = new grpc.Metadata;
    metadata.headersMap["Authorization"] = [`Bearer ${localStorage.getItem(TOKEN_KEYWORD)}`];
    return metadata;
  }
}
