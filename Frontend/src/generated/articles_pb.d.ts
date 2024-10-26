// package: personalBlog
// file: articles.proto

import * as jspb from "google-protobuf";

export class IdMassage extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): IdMassage.AsObject;
  static toObject(includeInstance: boolean, msg: IdMassage): IdMassage.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: IdMassage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): IdMassage;
  static deserializeBinaryFromReader(message: IdMassage, reader: jspb.BinaryReader): IdMassage;
}

export namespace IdMassage {
  export type AsObject = {
    id: number,
  }
}

export class PagedListQuery extends jspb.Message {
  getPage(): number;
  setPage(value: number): void;

  getSize(): number;
  setSize(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PagedListQuery.AsObject;
  static toObject(includeInstance: boolean, msg: PagedListQuery): PagedListQuery.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PagedListQuery, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PagedListQuery;
  static deserializeBinaryFromReader(message: PagedListQuery, reader: jspb.BinaryReader): PagedListQuery;
}

export namespace PagedListQuery {
  export type AsObject = {
    page: number,
    size: number,
  }
}

export class PagedListResult extends jspb.Message {
  getPage(): number;
  setPage(value: number): void;

  getSize(): number;
  setSize(value: number): void;

  getTotalpages(): number;
  setTotalpages(value: number): void;

  getRowscount(): number;
  setRowscount(value: number): void;

  clearItemsList(): void;
  getItemsList(): Array<ArticleShort>;
  setItemsList(value: Array<ArticleShort>): void;
  addItems(value?: ArticleShort, index?: number): ArticleShort;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PagedListResult.AsObject;
  static toObject(includeInstance: boolean, msg: PagedListResult): PagedListResult.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PagedListResult, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PagedListResult;
  static deserializeBinaryFromReader(message: PagedListResult, reader: jspb.BinaryReader): PagedListResult;
}

export namespace PagedListResult {
  export type AsObject = {
    page: number,
    size: number,
    totalpages: number,
    rowscount: number,
    itemsList: Array<ArticleShort.AsObject>,
  }
}

export class ArticleCreate extends jspb.Message {
  getTitle(): string;
  setTitle(value: string): void;

  getText(): string;
  setText(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ArticleCreate.AsObject;
  static toObject(includeInstance: boolean, msg: ArticleCreate): ArticleCreate.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ArticleCreate, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ArticleCreate;
  static deserializeBinaryFromReader(message: ArticleCreate, reader: jspb.BinaryReader): ArticleCreate;
}

export namespace ArticleCreate {
  export type AsObject = {
    title: string,
    text: string,
  }
}

export class ArticleEdit extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getTitle(): string;
  setTitle(value: string): void;

  getText(): string;
  setText(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ArticleEdit.AsObject;
  static toObject(includeInstance: boolean, msg: ArticleEdit): ArticleEdit.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ArticleEdit, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ArticleEdit;
  static deserializeBinaryFromReader(message: ArticleEdit, reader: jspb.BinaryReader): ArticleEdit;
}

export namespace ArticleEdit {
  export type AsObject = {
    id: number,
    title: string,
    text: string,
  }
}

export class ArticleShort extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getTitle(): string;
  setTitle(value: string): void;

  getText(): string;
  setText(value: string): void;

  getLiked(): boolean;
  setLiked(value: boolean): void;

  getPreviewurl(): string;
  setPreviewurl(value: string): void;

  getLikescount(): number;
  setLikescount(value: number): void;

  getCommentscount(): number;
  setCommentscount(value: number): void;

  getIspublished(): boolean;
  setIspublished(value: boolean): void;

  getAuthorid(): number;
  setAuthorid(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ArticleShort.AsObject;
  static toObject(includeInstance: boolean, msg: ArticleShort): ArticleShort.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ArticleShort, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ArticleShort;
  static deserializeBinaryFromReader(message: ArticleShort, reader: jspb.BinaryReader): ArticleShort;
}

export namespace ArticleShort {
  export type AsObject = {
    id: number,
    title: string,
    text: string,
    liked: boolean,
    previewurl: string,
    likescount: number,
    commentscount: number,
    ispublished: boolean,
    authorid: number,
  }
}

export class ArticleInfo extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getTitle(): string;
  setTitle(value: string): void;

  getText(): string;
  setText(value: string): void;

  getIspublished(): boolean;
  setIspublished(value: boolean): void;

  clearMediaurlsList(): void;
  getMediaurlsList(): Array<string>;
  setMediaurlsList(value: Array<string>): void;
  addMediaurls(value: string, index?: number): string;

  getLiked(): boolean;
  setLiked(value: boolean): void;

  getLikescount(): number;
  setLikescount(value: number): void;

  getCommentscount(): number;
  setCommentscount(value: number): void;

  clearCommentsList(): void;
  getCommentsList(): Array<Comment>;
  setCommentsList(value: Array<Comment>): void;
  addComments(value?: Comment, index?: number): Comment;

  getAuthorid(): number;
  setAuthorid(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ArticleInfo.AsObject;
  static toObject(includeInstance: boolean, msg: ArticleInfo): ArticleInfo.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ArticleInfo, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ArticleInfo;
  static deserializeBinaryFromReader(message: ArticleInfo, reader: jspb.BinaryReader): ArticleInfo;
}

export namespace ArticleInfo {
  export type AsObject = {
    id: number,
    title: string,
    text: string,
    ispublished: boolean,
    mediaurlsList: Array<string>,
    liked: boolean,
    likescount: number,
    commentscount: number,
    commentsList: Array<Comment.AsObject>,
    authorid: number,
  }
}

export class Comment extends jspb.Message {
  getAuthorid(): number;
  setAuthorid(value: number): void;

  getAuthorname(): string;
  setAuthorname(value: string): void;

  getAuthoravatar(): string;
  setAuthoravatar(value: string): void;

  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Comment.AsObject;
  static toObject(includeInstance: boolean, msg: Comment): Comment.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Comment, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Comment;
  static deserializeBinaryFromReader(message: Comment, reader: jspb.BinaryReader): Comment;
}

export namespace Comment {
  export type AsObject = {
    authorid: number,
    authorname: string,
    authoravatar: string,
    message: string,
  }
}

export class FileChunk extends jspb.Message {
  getArticleid(): number;
  setArticleid(value: number): void;

  getTotalchunks(): number;
  setTotalchunks(value: number): void;

  getFile(): Uint8Array | string;
  getFile_asU8(): Uint8Array;
  getFile_asB64(): string;
  setFile(value: Uint8Array | string): void;

  getFilename(): string;
  setFilename(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FileChunk.AsObject;
  static toObject(includeInstance: boolean, msg: FileChunk): FileChunk.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: FileChunk, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FileChunk;
  static deserializeBinaryFromReader(message: FileChunk, reader: jspb.BinaryReader): FileChunk;
}

export namespace FileChunk {
  export type AsObject = {
    articleid: number,
    totalchunks: number,
    file: Uint8Array | string,
    filename: string,
  }
}

export class RemoveFile extends jspb.Message {
  getArticleid(): number;
  setArticleid(value: number): void;

  getFileurl(): string;
  setFileurl(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RemoveFile.AsObject;
  static toObject(includeInstance: boolean, msg: RemoveFile): RemoveFile.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: RemoveFile, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RemoveFile;
  static deserializeBinaryFromReader(message: RemoveFile, reader: jspb.BinaryReader): RemoveFile;
}

export namespace RemoveFile {
  export type AsObject = {
    articleid: number,
    fileurl: string,
  }
}

export class Empty extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Empty.AsObject;
  static toObject(includeInstance: boolean, msg: Empty): Empty.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Empty, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Empty;
  static deserializeBinaryFromReader(message: Empty, reader: jspb.BinaryReader): Empty;
}

export namespace Empty {
  export type AsObject = {
  }
}

