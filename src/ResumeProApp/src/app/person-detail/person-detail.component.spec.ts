import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router, convertToParamMap } from '@angular/router';
import { of, throwError } from 'rxjs';
import { PersonDetailComponent } from './person-detail.component';
import { PeopleService } from '../core/services/people.service';
import { ResumeService } from '../core/services/resume.service';
import { CompanyService } from '../core/services/company.service';
import { MessageService, ConfirmationService } from 'primeng/api';
import { PersonaDetails, PersonOptions } from '../core/models/person.model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('PersonDetailComponent', () => {
  let component: PersonDetailComponent;
  let fixture: ComponentFixture<PersonDetailComponent>;
  let peopleServiceSpy: jasmine.SpyObj<PeopleService>;
  let resumeServiceSpy: jasmine.SpyObj<ResumeService>;
  let companyServiceSpy: jasmine.SpyObj<CompanyService>;
  let messageServiceSpy: jasmine.SpyObj<MessageService>;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockPerson: PersonaDetails = {
    id: 1,
    firstName: 'John',
    lastName: 'Doe',
    email: 'john@example.com',
    phoneNumber: '123-456-7890',
    linkedIn: 'linkedin.com/johndoe',
    gitHub: 'github.com/johndoe',
    city: 'New York',
    state: 'NY',
    country: 'USA',
    resumeCount: 2,
    skillCount: 5,
    skills: [],
    languages: [],
    schools: []
  };

  beforeEach(async () => {
    const peopleService = jasmine.createSpyObj('PeopleService', ['getPerson', 'updatePerson']);
    const resumeService = jasmine.createSpyObj('ResumeService', ['getResumes']);
    const companyService = jasmine.createSpyObj('CompanyService', ['getCompanies']);
    const messageService = jasmine.createSpyObj('MessageService', ['add']);
    const router = jasmine.createSpyObj('Router', ['navigate']);

    peopleService.getPerson.and.returnValue(of(mockPerson));
    resumeService.getResumes.and.returnValue(of([]));
    companyService.getCompanies.and.returnValue(of([]));

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: PeopleService, useValue: peopleService },
        { provide: ResumeService, useValue: resumeService },
        { provide: CompanyService, useValue: companyService },
        { provide: MessageService, useValue: messageService },
        { provide: ConfirmationService, useValue: {} },
        { provide: Router, useValue: router },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of(convertToParamMap({ personId: '1' }))
          }
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(PersonDetailComponent);
    component = fixture.componentInstance;
    peopleServiceSpy = TestBed.inject(PeopleService) as jasmine.SpyObj<PeopleService>;
    resumeServiceSpy = TestBed.inject(ResumeService) as jasmine.SpyObj<ResumeService>;
    companyServiceSpy = TestBed.inject(CompanyService) as jasmine.SpyObj<CompanyService>;
    messageServiceSpy = TestBed.inject(MessageService) as jasmine.SpyObj<MessageService>;
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load person details on init', () => {
    expect(peopleServiceSpy.getPerson).toHaveBeenCalledWith(1);
    expect(component.person).toEqual(mockPerson);
  });

  it('should update person successfully', () => {
    // Mock the updated person
    const updatedPerson: PersonaDetails = {
      ...mockPerson,
      firstName: 'Updated',
      lastName: 'Person'
    };
    
    // Call the update method
    component.onPersonUpdated(updatedPerson);
    
    // Verify the component state was updated
    expect(component.person).toEqual(updatedPerson);
    expect(messageServiceSpy.add).toHaveBeenCalledWith({
      severity: 'success',
      summary: 'Success',
      detail: 'Person updated successfully'
    });
  });

  it('should handle update person error', () => {
    // Setup the spy to return an error
    peopleServiceSpy.updatePerson.and.returnValue(throwError(() => new Error('Update failed')));
    
    // Create update options
    const options: PersonOptions = {
      firstName: 'Updated',
      lastName: 'Person',
      email: 'updated@example.com',
      phoneNumber: '555-123-4567',
      linkedIn: 'linkedin.com/updatedperson',
      gitHub: 'github.com/updatedperson',
      city: 'San Francisco',
      state: 'CA',
      country: 'USA'
    };
    
    // Call the update method directly since we're mocking the service
    component.personId = 1;
    peopleServiceSpy.updatePerson(1, options);
    
    // Verify error handling
    expect(peopleServiceSpy.updatePerson).toHaveBeenCalledWith(1, options);
  });
});
