import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Result } from '../models/base.model';
import { CompanyDetails, CompanyOptions } from '../models/company.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService extends ApiService {
  getCompanies(personId: number): Observable<CompanyDetails[]> {
    return this.get<CompanyDetails[]>(`/people/${personId}/companies`);
  }

  getCompany(personId: number, companyId: number): Observable<CompanyDetails> {
    return this.get<CompanyDetails>(`/people/${personId}/companies/${companyId}`);
  }

  createCompany(personId: number, options: CompanyOptions): Observable<CompanyDetails> {
    return this.post<CompanyDetails>(`/people/${personId}/companies`, options);
  }

  updateCompany(personId: number, companyId: number, options: CompanyOptions): Observable<CompanyDetails> {
    return this.put<CompanyDetails>(`/people/${personId}/companies/${companyId}`, options);
  }

  deleteCompany(personId: number, companyId: number): Observable<Result> {
    return this.delete<Result>(`/people/${personId}/companies/${companyId}`);
  }
}
