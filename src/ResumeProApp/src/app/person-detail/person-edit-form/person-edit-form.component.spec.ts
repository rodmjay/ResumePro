import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { PersonEditFormComponent } from './person-edit-form.component';
import { PeopleService } from '../../core/services/people.service';
import { MessageService } from 'primeng/api';
import { of, throwError } from 'rxjs';
import { PersonaDetails } from '../../core/models/person.model';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('PersonEditFormComponent', () => {
  let component: PersonEditFormComponent;
  let fixture: ComponentFixture<PersonEditFormComponent>;
  let peopleServiceSpy: jasmine.SpyObj<PeopleService>;
  let messageServiceSpy: jasmine.SpyObj<MessageService>;

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
    const peopleService = jasmine.createSpyObj('PeopleService', ['updatePerson']);
    const messageService = jasmine.createSpyObj('MessageService', ['add']);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [],
      providers: [
        FormBuilder,
        { provide: PeopleService, useValue: peopleService },
        { provide: MessageService, useValue: messageService }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(PersonEditFormComponent);
    component = fixture.componentInstance;
    peopleServiceSpy = TestBed.inject(PeopleService) as jasmine.SpyObj<PeopleService>;
    messageServiceSpy = TestBed.inject(MessageService) as jasmine.SpyObj<MessageService>;
    
    // Set input properties
    component.person = mockPerson;
    component.personId = 1;
    
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form with person data', () => {
    component.ngOnInit();
    expect(component.personForm.get('firstName')?.value).toBe(mockPerson.firstName);
    expect(component.personForm.get('lastName')?.value).toBe(mockPerson.lastName);
    expect(component.personForm.get('email')?.value).toBe(mockPerson.email);
  });

  it('should validate required fields', () => {
    component.ngOnInit();
    
    // Set invalid values
    component.personForm.get('firstName')?.setValue('');
    component.personForm.get('lastName')?.setValue('');
    component.personForm.get('email')?.setValue('');
    
    // Form should be invalid
    expect(component.personForm.valid).toBeFalse();
    
    // Set valid values
    component.personForm.get('firstName')?.setValue('John');
    component.personForm.get('lastName')?.setValue('Doe');
    component.personForm.get('email')?.setValue('john@example.com');
    
    // Form should be valid
    expect(component.personForm.valid).toBeTrue();
  });

  it('should submit form and update person successfully', () => {
    // Setup the spy to return the updated person
    const updatedPerson = { ...mockPerson, firstName: 'Updated' };
    peopleServiceSpy.updatePerson.and.returnValue(of(updatedPerson));
    
    // Initialize form and set values
    component.ngOnInit();
    component.personForm.get('firstName')?.setValue('Updated');
    
    // Create spy for output event
    spyOn(component.personUpdated, 'emit');
    
    // Submit form
    component.onSubmit();
    
    // Verify service was called with correct data
    expect(peopleServiceSpy.updatePerson).toHaveBeenCalled();
    
    // Verify success message was shown
    expect(messageServiceSpy.add).toHaveBeenCalledWith({
      severity: 'success',
      summary: 'Success',
      detail: 'Person updated successfully'
    });
    
    // Verify output event was emitted
    expect(component.personUpdated.emit).toHaveBeenCalledWith(updatedPerson);
  });

  it('should handle update error', () => {
    // Setup the spy to return an error
    peopleServiceSpy.updatePerson.and.returnValue(throwError(() => new Error('Update failed')));
    
    // Initialize form
    component.ngOnInit();
    
    // Submit form
    component.onSubmit();
    
    // Verify error message was shown
    expect(messageServiceSpy.add).toHaveBeenCalledWith({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to update person. Please try again.'
    });
  });
});
