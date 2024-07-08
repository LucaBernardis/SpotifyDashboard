import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiListComponent } from './multi-list.component';

describe('TopGenresComponent', () => {
  let component: MultiListComponent;
  let fixture: ComponentFixture<MultiListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MultiListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultiListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
