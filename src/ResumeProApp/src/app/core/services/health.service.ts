import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, timer } from 'rxjs';
import { switchMap, tap, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

export enum ApiStatus {
  Up = 'up',
  Down = 'down',
  Checking = 'checking'
}

@Injectable({
  providedIn: 'root'
})
export class HealthService {
  private apiUrl = environment.apiUrl;
  private statusSubject = new BehaviorSubject<ApiStatus>(ApiStatus.Checking);
  public status$ = this.statusSubject.asObservable();
  
  constructor(private http: HttpClient) {
    // Check API status every 30 seconds
    timer(0, 30000).pipe(
      switchMap(() => this.checkHealth())
    ).subscribe();
  }
  
  checkHealth(): Observable<any> {
    this.statusSubject.next(ApiStatus.Checking);
    return this.http.get(`${this.apiUrl}/health`).pipe(
      tap(() => {
        this.statusSubject.next(ApiStatus.Up);
      }),
      catchError(error => {
        this.statusSubject.next(ApiStatus.Down);
        return [];
      })
    );
  }
}
