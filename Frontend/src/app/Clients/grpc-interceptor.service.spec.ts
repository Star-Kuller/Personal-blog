import { TestBed } from '@angular/core/testing';

import { GrpcInterceptorService } from './grpc-interceptor.service';

describe('GrpcInterceptorService', () => {
  let service: GrpcInterceptorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GrpcInterceptorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
