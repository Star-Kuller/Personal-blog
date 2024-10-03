// package: personalBlog
// file: auth.proto

import * as jspb from "google-protobuf";

export class LoginForm extends jspb.Message {
  getUsername(): string;
  setUsername(value: string): void;

  getPassword(): string;
  setPassword(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): LoginForm.AsObject;
  static toObject(includeInstance: boolean, msg: LoginForm): LoginForm.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: LoginForm, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): LoginForm;
  static deserializeBinaryFromReader(message: LoginForm, reader: jspb.BinaryReader): LoginForm;
}

export namespace LoginForm {
  export type AsObject = {
    username: string,
    password: string,
  }
}

export class RegisterForm extends jspb.Message {
  getUsername(): string;
  setUsername(value: string): void;

  getPassword(): string;
  setPassword(value: string): void;

  getPasswordconfirm(): string;
  setPasswordconfirm(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RegisterForm.AsObject;
  static toObject(includeInstance: boolean, msg: RegisterForm): RegisterForm.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: RegisterForm, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RegisterForm;
  static deserializeBinaryFromReader(message: RegisterForm, reader: jspb.BinaryReader): RegisterForm;
}

export namespace RegisterForm {
  export type AsObject = {
    username: string,
    password: string,
    passwordconfirm: string,
  }
}

export class TokenResponse extends jspb.Message {
  getToken(): string;
  setToken(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TokenResponse.AsObject;
  static toObject(includeInstance: boolean, msg: TokenResponse): TokenResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TokenResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TokenResponse;
  static deserializeBinaryFromReader(message: TokenResponse, reader: jspb.BinaryReader): TokenResponse;
}

export namespace TokenResponse {
  export type AsObject = {
    token: string,
  }
}

