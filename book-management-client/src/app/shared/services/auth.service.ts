import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { rehydrateApplicationState } from 'ngrx-store-localstorage';
import jwt_decode from "jwt-decode";
import { NgxPermissionsService } from 'ngx-permissions';

import { environment } from '../../../environments/environment';
import { Observable, lastValueFrom } from 'rxjs';
import { IAuth } from '../interfaces/auth.interface';
import { User, UserTokenModel } from '../models/user.model';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.serverUrl;
  private loggedUserId!: string;
  private user!: User;

  constructor(private httpClient: HttpClient, private userService: UserService, private ngxPermissionsService: NgxPermissionsService) { }

  onAuthLogin = (authPayload: IAuth): Observable<UserTokenModel> => {
    return this.httpClient.post<UserTokenModel>(`${this.baseUrl}/api/v1/auth/login`, authPayload);
  }

  getAuthToken = (): string => {
    const tokenInfo = <UserTokenModel>rehydrateApplicationState(['userTokenInfo'], localStorage, k => k, true)?.userTokenInfo;
    return tokenInfo.authToken;
  }

  getStoredToken = async (): Promise<UserTokenModel> => {
    const tokenInfo = <UserTokenModel>rehydrateApplicationState(['userTokenInfo'], localStorage, k => k, true)?.userTokenInfo;
    await this.processAuthConfiguration(tokenInfo)
    return tokenInfo;
  }

  processAuthConfiguration = async (tokenModel: UserTokenModel): Promise<void> => {
    var userResults: any = jwt_decode(tokenModel.authToken);
    if (userResults) {
      this.loggedUserId = userResults?.userId;
      this.user = await lastValueFrom(this.userService.getUser(this.loggedUserId));
      const permissions: string[] = this.user?.role?.permissions?.map((x: any) => x.permissionName);
      this.ngxPermissionsService.loadPermissions(permissions);
    }
  }

  getLoggedUserId = (): string => {
    return this.loggedUserId;
  }
}
