import {grpc} from "@improbable-eng/grpc-web";

export class RpcError extends Error {
  public code : grpc.Code;
  public metadata : grpc.Metadata;

  constructor(message : string, code: grpc.Code, metadata: grpc.Metadata) {
    super(message);
    this.code = code;
    this.metadata = metadata;
  }
}
