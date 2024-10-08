import { TestBed } from '@angular/core/testing';
import {SystemMassageService} from "./system-massages.service";

describe('ErrorServiceService', () => {
  let service: SystemMassageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SystemMassageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
