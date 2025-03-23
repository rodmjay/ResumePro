import { ActionReducerMap } from '@ngrx/store';
import * as fromApi from './api/api.reducer';

export interface AppState {
  api: fromApi.ApiState;
}

export const reducers: ActionReducerMap<AppState> = {
  api: fromApi.apiReducer
};
