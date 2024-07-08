import { Component, Input } from '@angular/core';
import { ListItem } from '../../models/listItem';


@Component({
  selector: 'app-user-playlist',
  standalone: true,
  imports: [],
  templateUrl: './user-playlist.component.html',
  styleUrl: './user-playlist.component.css'
})
export class UserPlaylistComponent{

  @Input() playlists: ListItem[] = [];
  @Input() label: string = '';

  constructor(){}
  // When u click on a playlist it redirects you to the spotify page of that playlist
  spotifyRedirect(playlist: ListItem){
    window.open(playlist.spotifyUrl, '_blank');
  }

}
