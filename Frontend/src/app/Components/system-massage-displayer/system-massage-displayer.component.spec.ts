import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemMassageDisplayerComponent } from './system-massage-displayer.component';

describe('SystemMassageDisplayerComponent', () => {
  let component: SystemMassageDisplayerComponent;
  let fixture: ComponentFixture<SystemMassageDisplayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemMassageDisplayerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SystemMassageDisplayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
