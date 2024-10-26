import { TestBed } from '@angular/core/testing';

import { ArticlesClientService } from './articles-client.service';

describe('ArticlesService', () => {
  let service: ArticlesClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArticlesClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
