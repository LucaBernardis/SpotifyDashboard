import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopArtistSongComponent } from './top-artist-song.component';

describe('TopArtistSongComponent', () => {
  let component: TopArtistSongComponent;
  let fixture: ComponentFixture<TopArtistSongComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopArtistSongComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopArtistSongComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
