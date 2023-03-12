import { ActionReducer, MetaReducer } from '@ngrx/store';

import { localStorageSync } from 'ngrx-store-localstorage';
import { UserActions } from '../actions/user-action';
import { UserActionTypes } from '../action-types/user-action-types';


export const initialState = {
  userTokenInfo: undefined,
  user: undefined,
};

export function userReducer(state = initialState, action: UserActions) {
  switch (action.type) {
    case UserActionTypes.ADD_USER:
      return {
        ...state,
        userTokenInfo: action.payload,
      }
    case UserActionTypes.ADD_USER_TOKEN:
      return {
        ...state,
        userTokenInfo: action.payload,
      }
    default:
      return {
        ...state
      }
  }
}

export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return localStorageSync({keys: ['userTokenInfo']})(reducer);
}

export const metaReducers: Array<MetaReducer<any, any>> = [localStorageSyncReducer];
