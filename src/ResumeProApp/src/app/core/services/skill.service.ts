import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { SkillDto } from '../models/skill.model';

@Injectable({
  providedIn: 'root'
})
export class SkillService extends ApiService {
  getSkills(): Observable<SkillDto[]> {
    return this.get<SkillDto[]>('/skills');
  }
}
