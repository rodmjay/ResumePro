import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { PagedList, PagingQuery, Result } from '../models/base.model';
import { PersonOptions, PersonaDetails, PersonaDto, PersonaFilters } from '../models/person.model';

@Injectable({
  providedIn: 'root'
})
export class PeopleService extends ApiService {
  private baseUrl = '/people';

  getPeople(filters?: PersonaFilters, paging?: PagingQuery): Observable<PagedList<PersonaDto>> {
    return this.post<PagedList<PersonaDto>>(`${this.baseUrl}/Search`, filters || {}, paging);
  }

  getPerson(personId: number): Observable<PersonaDetails> {
    return this.get<PersonaDetails>(`${this.baseUrl}/${personId}`);
  }

  createPerson(options: PersonOptions): Observable<PersonaDetails> {
    return this.post<PersonaDetails>(this.baseUrl, options);
  }

  updatePerson(personId: number, options: PersonOptions): Observable<PersonaDetails> {
    return this.put<PersonaDetails>(`${this.baseUrl}/${personId}`, options);
  }

  deletePerson(personId: number): Observable<Result> {
    return this.delete<Result>(`${this.baseUrl}/${personId}`);
  }
  
  // Skills management
  getSkills(personId: number): Observable<any[]> {
    return this.get<any[]>(`${this.baseUrl}/${personId}/skills`);
  }
  
  toggleSkill(personId: number, skillId: number): Observable<Result> {
    return this.post<Result>(`${this.baseUrl}/${personId}/skills/${skillId}/toggle`, {});
  }
  
  // Languages management
  getLanguages(personId: number): Observable<any[]> {
    return this.get<any[]>(`${this.baseUrl}/${personId}/languages`);
  }
  
  toggleLanguage(personId: number, languageCode: string, proficiency: number): Observable<Result> {
    return this.post<Result>(`${this.baseUrl}/${personId}/languages/${languageCode}/toggle`, { proficiency });
  }
}
