import { Injectable } from '@angular/core';
import { GrpcEvent, GrpcMessage, GrpcRequest } from '@ngx-grpc/common';
import { GrpcHandler, GrpcInterceptor,  } from '@ngx-grpc/core';
import { Observable } from 'rxjs';

@Injectable()
export class GrpcInterceptorHandler implements GrpcInterceptor {
  constructor() {
    console.log('GrpcInterceptorHandler constructed');
  }
  intercept<Q extends GrpcMessage, S extends GrpcMessage>(request: GrpcRequest<Q, S>, next: GrpcHandler): Observable<GrpcEvent<S>> {
    // Modify the request metadata here
    request.requestMetadata.set('Authorization', 'Bearer ' + '');

    // Log the request and metadata for debugging purposes
    console.log('gRPC request:', request);
    console.log('gRPC metadata:', request.requestMetadata);

    // Forward the request to the next handler in the chain
    return next.handle(request);
  }
}
