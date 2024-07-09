import { Component, Input } from '@angular/core';
import { ListItem } from '../../models/listItem';

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [],

  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent{

  @Input() items: ListItem[] = [];
  @Input() label: string = '';


  constructor() { }


  // Pass the track values to the player component
  playSong(song: ListItem){
    window.open(song.spotifyUrl, '_blank');
  }
}


