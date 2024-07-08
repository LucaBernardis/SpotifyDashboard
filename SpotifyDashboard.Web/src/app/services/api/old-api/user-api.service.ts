import { Injectable } from '@angular/core';
import { UserService } from '../../old-services/user.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  // Backend api EndPoint for user data
  private readonly _baseUrl = '/serverApi/me';

  // Shared Headers
  private httpheaders = new HttpHeaders({
    'Authorization': `Bearer ${this.userService.getToken()}`,
    'Content-Type': 'application/json',
  });

  constructor(private http: HttpClient, private userService: UserService) { }

  // Get the data from the user
  getUserData(): Observable<any> {
    return this.http.get(`${this._baseUrl}/getData`, { headers: this.httpheaders });
  }

  // Get the user playlists
  getUserPlaylists(): Observable<any>{
    return this.http.get(`${this._baseUrl}/getPlaylists`, {headers: this.httpheaders});
  }
}