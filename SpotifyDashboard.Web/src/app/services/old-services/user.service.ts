import { Injectable } from '@angular/core';
import { BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // Shared access token value
  private access_token$: BehaviorSubject<string | null | undefined> = new BehaviorSubject<string | null | undefined>(null);

  constructor(){ }

  setToken(token: string | undefined){
    this.access_token$.next(token);
  }

  getToken(): string | undefined | null {
    return this.access_token$.value;
  }
}