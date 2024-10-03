// package: personalBlog
// file: auth.proto

import * as auth_pb from "./auth_pb";
import {grpc} from "@improbable-eng/grpc-web";

type Authorizationlogin = {
  readonly methodName: string;
  readonly service: typeof Authorization;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof auth_pb.LoginForm;
  readonly responseType: typeof auth_pb.TokenResponse;
};

type Authorizationregister = {
  readonly methodName: string;
  readonly service: typeof Authorization;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof auth_pb.RegisterForm;
  readonly responseType: typeof auth_pb.TokenResponse;
};

export class Authorization {
  static readonly serviceName: string;
  static readonly login: Authorizationlogin;
  static readonly register: Authorizationregister;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class AuthorizationClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  login(
    requestMessage: auth_pb.LoginForm,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: auth_pb.TokenResponse|null) => void
  ): UnaryResponse;
  login(
    requestMessage: auth_pb.LoginForm,
    callback: (error: ServiceError|null, responseMessage: auth_pb.TokenResponse|null) => void
  ): UnaryResponse;
  register(
    requestMessage: auth_pb.RegisterForm,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: auth_pb.TokenResponse|null) => void
  ): UnaryResponse;
  register(
    requestMessage: auth_pb.RegisterForm,
    callback: (error: ServiceError|null, responseMessage: auth_pb.TokenResponse|null) => void
  ): UnaryResponse;
}

