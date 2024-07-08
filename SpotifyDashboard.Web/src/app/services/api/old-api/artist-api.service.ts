import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Artist } from '../../../models/artist';
import { ArtistService } from '../../old-services/artist.service';
import { Track } from '../../../models/track';
import { Album } from '../../../models/album';



@Injectable({
  providedIn: 'root'
})
export class ArtistApiService {

  // shared Headers
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${this.artistService.getToken()}`,
  });

  // Backend api EndPoint
  private _baseUrl:string = '/serverApi/artist';

  constructor(private http: HttpClient, private artistService: ArtistService) { }


  // Get the data of the user favourite artist
  getUserTopArtist(): Observable<Artist>{
    return this.http.get<Artist>(this._baseUrl + '/topArtist', {headers: this.httpHeaders});
  }

  // Get to favourtie artist most famous track
  getArtistTopTrack(id: string): Observable<Track>{
    return this.http.get<Track>(`${this._baseUrl}/topArtistTrack/${id}`, {headers: this.httpHeaders})
  }

  // Get the albums of the favourite artist
  getArtistAlbums(id: string): Observable<any>{
    return this.http.get<any>(`${this._baseUrl}/getAlbums/${id}`, {headers: this.httpHeaders})
  }

  // Get the spotify new releases (albums)
  getNewReleases(): Observable<Album[]>{
    return this.http.get<Album[]>(`${this._baseUrl}/newReleases`, {headers: this.httpHeaders});
  }
}
