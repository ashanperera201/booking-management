import { Action } from '@ngrx/store';
import { UserActionTypes } from '../action-types/user-action-types';
import { User, UserTokenModel } from '../../shared/models/user.model';

export class AddUserToken implements Action {
  readonly type = UserActionTypes.ADD_USER_TOKEN;
  constructor(public payload: UserTokenModel) { }
}

export class AddUser implements Action {
  readonly type = UserActionTypes.ADD_USER;
  constructor(public payload: User) { }
}

export type UserActions = AddUserToken | AddUser
