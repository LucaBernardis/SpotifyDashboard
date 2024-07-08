import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-top-artist-song',
  standalone: true,
  imports: [],
  templateUrl: './top-artist-song.component.html',
  styleUrl: './top-artist-song.component.css'
})
export class TopArtistSongComponent{

  @Input() artist: string = '';
  @Input() image: string = '';
  @Input() trackName: string = '';
  @Input() label: string= '';

  constructor(){}

}
