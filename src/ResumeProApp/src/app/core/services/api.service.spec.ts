import { HttpTestingController } from '@angular/common/http/testing';
import { expectRequest, setupTestBed } from '../tests/test-utils';
import { ApiService } from './api.service';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// Create a test service that extends ApiService to test protected methods
@Injectable({ providedIn: 'root' })
class TestApiService extends ApiService {
  public testGet<T>(url: string, params?: any): Observable<T> {
    return this.get<T>(url, params);
  }

  public testPost<T>(url: string, body: any): Observable<T> {
    return this.post<T>(url, body);
  }

  public testPut<T>(url: string, body: any): Observable<T> {
    return this.put<T>(url, body);
  }

  public testDelete<T>(url: string): Observable<T> {
    return this.delete<T>(url);
  }
}

describe('ApiService', () => {
  let service: TestApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    const testBed = setupTestBed<TestApiService>(TestApiService);
    service = testBed.service as TestApiService;
    httpMock = testBed.httpMock;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should perform GET request', () => {
    const mockData = { id: 1, name: 'Test' };
    
    service.testGet('/test').subscribe(data => {
      expect(data).toEqual(mockData);
    });

    const req = expectRequest(httpMock, 'GET', '/test');
    req.flush(mockData);
  });

  it('should perform POST request', () => {
    const mockData = { id: 1, name: 'Test' };
    const payload = { name: 'Test' };
    
    service.testPost('/test', payload).subscribe(data => {
      expect(data).toEqual(mockData);
    });

    const req = expectRequest(httpMock, 'POST', '/test', payload);
    req.flush(mockData);
  });

  it('should perform PUT request', () => {
    const mockData = { id: 1, name: 'Updated Test' };
    const payload = { name: 'Updated Test' };
    
    service.testPut('/test/1', payload).subscribe(data => {
      expect(data).toEqual(mockData);
    });

    const req = expectRequest(httpMock, 'PUT', '/test/1', payload);
    req.flush(mockData);
  });

  it('should perform DELETE request', () => {
    const mockData = { succeeded: true, errors: [] };
    
    service.testDelete('/test/1').subscribe(data => {
      expect(data).toEqual(mockData);
    });

    const req = expectRequest(httpMock, 'DELETE', '/test/1');
    req.flush(mockData);
  });

  it('should handle error responses', () => {
    const errorResponse = { status: 404, statusText: 'Not Found' };
    const errorMessage = 'Http failure response for ' + environment.apiUrl + '/test: 404 Not Found';
    
    service.testGet('/test').subscribe({
      next: () => fail('should have failed with 404 error'),
      error: (error) => {
        expect(error.message).toContain(errorMessage);
      }
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/test`);
    req.flush('Not Found', errorResponse);
  });
});
