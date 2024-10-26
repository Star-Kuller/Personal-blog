// package: personalBlog
// file: articles.proto

import * as articles_pb from "./articles_pb";
import {grpc} from "@improbable-eng/grpc-web";

type ArticlesgetPagedList = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.PagedListQuery;
  readonly responseType: typeof articles_pb.PagedListResult;
};

type Articlescreate = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.ArticleCreate;
  readonly responseType: typeof articles_pb.IdMassage;
};

type Articlesedit = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.ArticleEdit;
  readonly responseType: typeof articles_pb.Empty;
};

type Articlesdelete = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.IdMassage;
  readonly responseType: typeof articles_pb.Empty;
};

type Articlesview = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.IdMassage;
  readonly responseType: typeof articles_pb.ArticleInfo;
};

type ArticlesuploadFile = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: true;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.FileChunk;
  readonly responseType: typeof articles_pb.Empty;
};

type ArticlesremoveFile = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.RemoveFile;
  readonly responseType: typeof articles_pb.Empty;
};

type Articlespublish = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.IdMassage;
  readonly responseType: typeof articles_pb.Empty;
};

type Articleslike = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.IdMassage;
  readonly responseType: typeof articles_pb.Empty;
};

type Articlesunlike = {
  readonly methodName: string;
  readonly service: typeof Articles;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof articles_pb.IdMassage;
  readonly responseType: typeof articles_pb.Empty;
};

export class Articles {
  static readonly serviceName: string;
  static readonly getPagedList: ArticlesgetPagedList;
  static readonly create: Articlescreate;
  static readonly edit: Articlesedit;
  static readonly delete: Articlesdelete;
  static readonly view: Articlesview;
  static readonly uploadFile: ArticlesuploadFile;
  static readonly removeFile: ArticlesremoveFile;
  static readonly publish: Articlespublish;
  static readonly like: Articleslike;
  static readonly unlike: Articlesunlike;
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

export class ArticlesClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  getPagedList(
    requestMessage: articles_pb.PagedListQuery,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.PagedListResult|null) => void
  ): UnaryResponse;
  getPagedList(
    requestMessage: articles_pb.PagedListQuery,
    callback: (error: ServiceError|null, responseMessage: articles_pb.PagedListResult|null) => void
  ): UnaryResponse;
  create(
    requestMessage: articles_pb.ArticleCreate,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.IdMassage|null) => void
  ): UnaryResponse;
  create(
    requestMessage: articles_pb.ArticleCreate,
    callback: (error: ServiceError|null, responseMessage: articles_pb.IdMassage|null) => void
  ): UnaryResponse;
  edit(
    requestMessage: articles_pb.ArticleEdit,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  edit(
    requestMessage: articles_pb.ArticleEdit,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  delete(
    requestMessage: articles_pb.IdMassage,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  delete(
    requestMessage: articles_pb.IdMassage,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  view(
    requestMessage: articles_pb.IdMassage,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.ArticleInfo|null) => void
  ): UnaryResponse;
  view(
    requestMessage: articles_pb.IdMassage,
    callback: (error: ServiceError|null, responseMessage: articles_pb.ArticleInfo|null) => void
  ): UnaryResponse;
  uploadFile(metadata?: grpc.Metadata): RequestStream<articles_pb.FileChunk>;
  removeFile(
    requestMessage: articles_pb.RemoveFile,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  removeFile(
    requestMessage: articles_pb.RemoveFile,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  publish(
    requestMessage: articles_pb.IdMassage,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  publish(
    requestMessage: articles_pb.IdMassage,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  like(
    requestMessage: articles_pb.IdMassage,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  like(
    requestMessage: articles_pb.IdMassage,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  unlike(
    requestMessage: articles_pb.IdMassage,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
  unlike(
    requestMessage: articles_pb.IdMassage,
    callback: (error: ServiceError|null, responseMessage: articles_pb.Empty|null) => void
  ): UnaryResponse;
}

