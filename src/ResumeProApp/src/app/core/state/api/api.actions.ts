import { createAction } from '@ngrx/store';

export const checkApiHealth = createAction('[API] Check Health');
export const apiHealthSuccess = createAction('[API] Health Check Success');
export const apiHealthFailure = createAction('[API] Health Check Failure');
