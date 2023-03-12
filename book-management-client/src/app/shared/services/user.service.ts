import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string = environment.serverUrl;

  constructor(private httpClient: HttpClient) { }

  getUser = (userId: string): Observable<User> => {
    return this.httpClient.get<User>(`${this.baseUrl}/api/v1/user/${userId}`);
  }
}
