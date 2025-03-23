import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Result } from '../models/base.model';
import { ResumeDetails, ResumeDto, ResumeOptions } from '../models/resume.model';

@Injectable({
  providedIn: 'root'
})
export class ResumeService extends ApiService {
  getResumes(personId: number): Observable<ResumeDto[]> {
    return this.get<ResumeDto[]>(`/people/${personId}/resumes`);
  }

  getResume(personId: number, resumeId: number): Observable<ResumeDetails> {
    return this.get<ResumeDetails>(`/people/${personId}/resumes/${resumeId}`);
  }

  createResume(personId: number, options: ResumeOptions): Observable<ResumeDetails> {
    return this.post<ResumeDetails>(`/people/${personId}/resumes`, options);
  }

  updateResume(personId: number, resumeId: number, options: ResumeOptions): Observable<ResumeDetails> {
    return this.put<ResumeDetails>(`/people/${personId}/resumes/${resumeId}`, options);
  }

  deleteResume(personId: number, resumeId: number): Observable<Result> {
    return this.delete<Result>(`/people/${personId}/resumes/${resumeId}`);
  }
}
