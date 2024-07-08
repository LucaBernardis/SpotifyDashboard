import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { Track } from '../../models/track';
import { ListItem } from '../../models/listItem';

@Injectable({
  providedIn: 'root'
})
export class TrackService {


  // Shared track value between top Songs & song player
  private currentTrackSubject = new BehaviorSubject<Track | null>(null);
  currentTrack$: Observable<Track | null> = this.currentTrackSubject.asObservable();

  // Shared access token value
  private access_token$: BehaviorSubject<string | null | undefined> = new BehaviorSubject<string | null | undefined>(null);

  constructor(){ }

  setToken(token: string | undefined){
    this.access_token$.next(token);
  }

  getToken(): string | undefined | null {
    return this.access_token$.value;
  }

  playSong(track: Track): void {
    this.currentTrackSubject.next(track);
  }

  
}
