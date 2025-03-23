import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ApiState } from './api.reducer';

export const selectApiState = createFeatureSelector<ApiState>('api');

export const selectIsApiOnline = createSelector(
  selectApiState,
  (state: ApiState) => state.isOnline
);

export const selectIsApiChecking = createSelector(
  selectApiState,
  (state: ApiState) => state.isChecking
);
