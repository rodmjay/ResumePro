import { HttpTestingController } from '@angular/common/http/testing';
import { expectRequest, setupTestBed } from '../tests/test-utils';
import { ResumeService } from './resume.service';
import { ResumeDetails, ResumeDto, ResumeOptions } from '../models/resume.model';

describe('ResumeService', () => {
  let service: ResumeService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    const testBed = setupTestBed<ResumeService>(ResumeService);
    service = testBed.service as ResumeService;
    httpMock = testBed.httpMock;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get resumes for a person', () => {
    const personId = 1;
    const mockResumes: ResumeDto[] = [
      {
        id: 1,
        personId: personId,
        organizationId: 1,
        settings: { showEmail: true, showPhone: true, showLinkedIn: true, showGitHub: true, showLocation: true },
        firstName: 'John',
        lastName: 'Doe',
        email: 'john@example.com',
        phoneNumber: '123-456-7890',
        linkedIn: 'linkedin.com/johndoe',
        gitHub: 'github.com/johndoe',
        city: 'New York',
        state: 'NY',
        country: 'USA',
        jobCount: 2,
        skillCount: 5,
        jobTitle: 'Software Engineer',
        description: 'Experienced software engineer'
      }
    ];

    service.getResumes(personId).subscribe(resumes => {
      expect(resumes).toEqual(mockResumes);
    });

    const req = expectRequest(httpMock, 'GET', `/people/${personId}/resumes`);
    req.flush(mockResumes);
  });

  it('should get a resume by id', () => {
    const personId = 1;
    const resumeId = 1;
    const mockResume: ResumeDetails = {
      id: resumeId,
      personId: personId,
      organizationId: 1,
      settings: { showEmail: true, showPhone: true, showLinkedIn: true, showGitHub: true, showLocation: true },
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      phoneNumber: '123-456-7890',
      linkedIn: 'linkedin.com/johndoe',
      gitHub: 'github.com/johndoe',
      city: 'New York',
      state: 'NY',
      country: 'USA',
      jobCount: 2,
      skillCount: 5,
      jobTitle: 'Software Engineer',
      description: 'Experienced software engineer',
      companies: [],
      skills: [],
      references: [],
      education: [],
      languages: [],
      certifications: [],
      renderings: [],
      skillDictionary: []
    };

    service.getResume(personId, resumeId).subscribe(resume => {
      expect(resume).toEqual(mockResume);
    });

    const req = expectRequest(httpMock, 'GET', `/people/${personId}/resumes/${resumeId}`);
    req.flush(mockResume);
  });

  it('should create a resume', () => {
    const personId = 1;
    const options: ResumeOptions = {
      settings: { showEmail: true, showPhone: true, showLinkedIn: true, showGitHub: true, showLocation: true },
      skillIds: [1, 2, 3],
      companyIds: [1, 2],
      description: 'Experienced software engineer',
      jobTitle: 'Software Engineer'
    };

    const mockResponse: ResumeDetails = {
      id: 1,
      personId: personId,
      organizationId: 1,
      settings: options.settings,
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      phoneNumber: '123-456-7890',
      linkedIn: 'linkedin.com/johndoe',
      gitHub: 'github.com/johndoe',
      city: 'New York',
      state: 'NY',
      country: 'USA',
      jobCount: 2,
      skillCount: 3,
      jobTitle: options.jobTitle,
      description: options.description,
      companies: [],
      skills: [],
      references: [],
      education: [],
      languages: [],
      certifications: [],
      renderings: [],
      skillDictionary: []
    };

    service.createResume(personId, options).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = expectRequest(httpMock, 'POST', `/people/${personId}/resumes`, options);
    req.flush(mockResponse);
  });

  it('should update a resume', () => {
    const personId = 1;
    const resumeId = 1;
    const options: ResumeOptions = {
      settings: { showEmail: true, showPhone: true, showLinkedIn: true, showGitHub: true, showLocation: true },
      skillIds: [1, 2, 3, 4],
      companyIds: [1, 2, 3],
      description: 'Updated description',
      jobTitle: 'Senior Software Engineer'
    };

    const mockResponse: ResumeDetails = {
      id: resumeId,
      personId: personId,
      organizationId: 1,
      settings: options.settings,
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      phoneNumber: '123-456-7890',
      linkedIn: 'linkedin.com/johndoe',
      gitHub: 'github.com/johndoe',
      city: 'New York',
      state: 'NY',
      country: 'USA',
      jobCount: 3,
      skillCount: 4,
      jobTitle: options.jobTitle,
      description: options.description,
      companies: [],
      skills: [],
      references: [],
      education: [],
      languages: [],
      certifications: [],
      renderings: [],
      skillDictionary: []
    };

    service.updateResume(personId, resumeId, options).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = expectRequest(httpMock, 'PUT', `/people/${personId}/resumes/${resumeId}`, options);
    req.flush(mockResponse);
  });

  it('should delete a resume', () => {
    const personId = 1;
    const resumeId = 1;
    const mockResponse = { succeeded: true, errors: [] };

    service.deleteResume(personId, resumeId).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = expectRequest(httpMock, 'DELETE', `/people/${personId}/resumes/${resumeId}`);
    req.flush(mockResponse);
  });
});
