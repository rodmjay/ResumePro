import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from '../../../environments/environment';

export function setupTestBed<T>(serviceType: any): { service: T; httpMock: HttpTestingController } {
  TestBed.configureTestingModule({
    imports: [HttpClientTestingModule],
    providers: [serviceType]
  });

  const service = TestBed.inject(serviceType) as T;
  const httpMock = TestBed.inject(HttpTestingController);

  return { service, httpMock };
}

export function expectRequest(
  httpMock: HttpTestingController,
  method: string,
  url: string,
  body?: any
): any {
  const req = httpMock.expectOne(`${environment.apiUrl}${url}`);
  expect(req.request.method).toEqual(method);
  
  if (body) {
    expect(req.request.body).toEqual(body);
  }
  
  return req;
}
