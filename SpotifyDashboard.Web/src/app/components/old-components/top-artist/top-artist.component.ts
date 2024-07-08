import { Component, Input, Output} from '@angular/core';

@Component({
  selector: 'app-top-artist',
  standalone: true,
  imports: [],
  templateUrl: './top-artist.component.html',
  styleUrl: './top-artist.component.css'
})
export class TopArtistComponent{

  @Input() artistName: string = '';
  @Input() image: string = '';
  @Input() label: string = ''

  constructor() { }

}
