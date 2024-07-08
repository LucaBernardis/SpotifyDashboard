import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Track } from '../../../models/track';
import { TrackService } from '../../old-services/track.service';

@Injectable({
  providedIn: 'root'
})
export class TracksApiService {

  // Shared Headers
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${this.trackService.getToken()}`,
    'Content-Type': 'application/json',
  });
  
  // Backend EndPoints for tracks
  private _baseURL:string = '/serverApi/track';

  constructor(private http: HttpClient, private trackService: TrackService) { }


  // Get the user top 20 listened songs
  getTopSongs(): Observable<Track[]> {
    return this.http.get<Track[]>(this._baseURL + '/topTenTracks', {headers: this.headers});
  }

  // Get recommended songs based on favourite artist and most famous track of the artist
  getRecommended(seedArtist: string, seedGenres: string, seedTracks: string): Observable<any>{
    return this.http.get<any>(`${this._baseURL}/getRecommended?`, {headers: this.headers, params: {seedArtist, seedGenres, seedTracks}});

  }

}
