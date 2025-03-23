import { createReducer, on } from '@ngrx/store';
import * as ApiActions from './api.actions';

export interface ApiState {
  isOnline: boolean;
  isChecking: boolean;
}

export const initialState: ApiState = {
  isOnline: false,
  isChecking: false
};

export const apiReducer = createReducer(
  initialState,
  on(ApiActions.checkApiHealth, state => ({
    ...state,
    isChecking: true
  })),
  on(ApiActions.apiHealthSuccess, state => ({
    ...state,
    isOnline: true,
    isChecking: false
  })),
  on(ApiActions.apiHealthFailure, state => ({
    ...state,
    isOnline: false,
    isChecking: false
  }))
);
