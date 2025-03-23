import { HttpTestingController } from '@angular/common/http/testing';
import { expectRequest, setupTestBed } from '../tests/test-utils';
import { SkillService } from './skill.service';
import { SkillDto } from '../models/skill.model';

describe('SkillService', () => {
  let service: SkillService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    const testBed = setupTestBed<SkillService>(SkillService);
    service = testBed.service as SkillService;
    httpMock = testBed.httpMock;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get skills', () => {
    const mockSkills: SkillDto[] = [
      { id: 1, title: 'JavaScript', categories: ['Programming'] },
      { id: 2, title: 'Angular', categories: ['Framework'] }
    ];

    service.getSkills().subscribe(skills => {
      expect(skills).toEqual(mockSkills);
    });

    const req = expectRequest(httpMock, 'GET', '/skills');
    req.flush(mockSkills);
  });
});
