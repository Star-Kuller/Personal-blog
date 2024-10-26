import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagedListComponent } from './paged-list.component';

describe('PagedListComponent', () => {
  let component: PagedListComponent;
  let fixture: ComponentFixture<PagedListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PagedListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PagedListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
