import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPlaylistComponent } from './user-playlist.component';

describe('UserPlaylistComponent', () => {
  let component: UserPlaylistComponent;
  let fixture: ComponentFixture<UserPlaylistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserPlaylistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserPlaylistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
