import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap } from 'rxjs/operators';
import { timer } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import * as ApiActions from './api.actions';

@Injectable()
export class ApiEffects {
  checkHealth$ = createEffect(() => this.actions$.pipe(
    ofType(ApiActions.checkApiHealth),
    switchMap(() => {
      // Use base URL without the /v1.0 prefix for health endpoint
      const baseUrl = environment.apiUrl.replace('/v1.0', '');
      return this.http.get(`${baseUrl}/health`).pipe(
        map(() => ApiActions.apiHealthSuccess()),
        catchError(() => [ApiActions.apiHealthFailure()])
      );
    })
  ));

  // Check health every 30 seconds
  periodicHealthCheck$ = createEffect(() => timer(0, 30000).pipe(
    map(() => ApiActions.checkApiHealth())
  ));

  constructor(
    private actions$: Actions,
    private http: HttpClient
  ) {}
}
