import { HttpTestingController } from '@angular/common/http/testing';
import { expectRequest, setupTestBed } from '../tests/test-utils';
import { CompanyService } from './company.service';
import { CompanyDetails, CompanyOptions } from '../models/company.model';
import { Result } from '../models/base.model';

describe('CompanyService', () => {
  let service: CompanyService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    const testBed = setupTestBed<CompanyService>(CompanyService);
    service = testBed.service as CompanyService;
    httpMock = testBed.httpMock;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get companies for a person', () => {
    const personId = 1;
    const mockCompanies: CompanyDetails[] = [
      {
        id: 1,
        company: 'Example Corp',
        description: 'A technology company',
        location: 'New York, NY',
        startDate: '2020-01-01',
        endDate: '2022-01-01',
        positionCount: 2,
        showTechnology: true,
        skills: [{ id: 1, title: 'JavaScript' }],
        positions: []
      }
    ];

    service.getCompanies(personId).subscribe(companies => {
      expect(companies).toEqual(mockCompanies);
    });

    const req = expectRequest(httpMock, 'GET', `/people/${personId}/companies`);
    req.flush(mockCompanies);
  });

  it('should get a company by id', () => {
    const personId = 1;
    const companyId = 1;
    const mockCompany: CompanyDetails = {
      id: companyId,
      company: 'Example Corp',
      description: 'A technology company',
      location: 'New York, NY',
      startDate: '2020-01-01',
      endDate: '2022-01-01',
      positionCount: 2,
      showTechnology: true,
      skills: [{ id: 1, title: 'JavaScript' }],
      positions: []
    };

    service.getCompany(personId, companyId).subscribe(company => {
      expect(company).toEqual(mockCompany);
    });

    const req = expectRequest(httpMock, 'GET', `/people/${personId}/companies/${companyId}`);
    req.flush(mockCompany);
  });

  it('should create a company', () => {
    const personId = 1;
    const options: CompanyOptions = {
      company: 'New Company',
      description: 'A new company',
      location: 'San Francisco, CA',
      startDate: '2022-01-01',
      endDate: '2023-01-01',
      positions: [],
      jobSkillIds: [1, 2, 3]
    };

    const mockResponse: CompanyDetails = {
      id: 1,
      company: options.company,
      description: options.description,
      location: options.location,
      startDate: options.startDate,
      endDate: options.endDate,
      positionCount: 0,
      showTechnology: true,
      skills: [],
      positions: []
    };

    service.createCompany(personId, options).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = expectRequest(httpMock, 'POST', `/people/${personId}/companies`, options);
    req.flush(mockResponse);
  });

  it('should update a company', () => {
    const personId = 1;
    const companyId = 1;
    const options: CompanyOptions = {
      company: 'Updated Company',
      description: 'An updated company',
      location: 'Boston, MA',
      startDate: '2022-01-01',
      endDate: '2023-06-01',
      positions: [],
      jobSkillIds: [1, 2, 3, 4]
    };

    const mockResponse: CompanyDetails = {
      id: companyId,
      company: options.company,
      description: options.description,
      location: options.location,
      startDate: options.startDate,
      endDate: options.endDate,
      positionCount: 0,
      showTechnology: true,
      skills: [],
      positions: []
    };

    service.updateCompany(personId, companyId, options).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = expectRequest(httpMock, 'PUT', `/people/${personId}/companies/${companyId}`, options);
    req.flush(mockResponse);
  });

  it('should delete a company', () => {
    const personId = 1;
    const companyId = 1;
    const mockResponse: Result = { succeeded: true, errors: [] };

    service.deleteCompany(personId, companyId).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = expectRequest(httpMock, 'DELETE', `/people/${personId}/companies/${companyId}`);
    req.flush(mockResponse);
  });
});
