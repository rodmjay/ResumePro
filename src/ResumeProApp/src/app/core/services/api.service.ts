import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  protected apiUrl = environment.apiUrl;
  protected organizationId = 1; // Default organization ID, can be updated from auth service

  constructor(protected http: HttpClient) {}

  protected get<T>(url: string, params?: any): Observable<T> {
    const options = { params: this.buildParams(params) };
    return this.http.get<T>(`${this.apiUrl}${url}`, options)
      .pipe(catchError(this.handleError));
  }

  protected post<T>(url: string, body: any, params?: any): Observable<T> {
    const options = params ? { params: this.buildParams(params) } : undefined;
    return this.http.post<T>(`${this.apiUrl}${url}`, body, options)
      .pipe(catchError(this.handleError));
  }

  protected put<T>(url: string, body: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}${url}`, body)
      .pipe(catchError(this.handleError));
  }

  protected delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}${url}`)
      .pipe(catchError(this.handleError));
  }

  private buildParams(params?: any): HttpParams {
    let httpParams = new HttpParams();
    if (params) {
      Object.keys(params).forEach(key => {
        if (params[key] !== null && params[key] !== undefined) {
          httpParams = httpParams.set(key, params[key]);
        }
      });
    }
    return httpParams;
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
