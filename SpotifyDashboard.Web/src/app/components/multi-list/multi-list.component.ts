import { Component, Input } from '@angular/core';
import { ListItem } from '../../models/listItem';

@Component({
  selector: 'app-multi-list',
  standalone: true,
  imports: [],
  templateUrl: './multi-list.component.html',
  styleUrl: './multi-list.component.css'
})
export class MultiListComponent {

  @Input() tracks: ListItem[] = []; // List of recommended tracks
  @Input() albums: ListItem[] = []; // list of artist albums

  constructor() { }

  // Redirect the user to the spotify page of the selected album
  albumRedirect(album: ListItem){
    window.open(album.spotifyUrl, '_blank')
  }

    // Redirect the user to the spotify page of the selected track
  trackRedirect(track: ListItem){
    window.open(track.spotifyUrl, '_blank');
  }


}
