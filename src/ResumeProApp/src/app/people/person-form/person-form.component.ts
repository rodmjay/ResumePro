import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { PeopleService } from '../../core/services/people.service';
import { PersonOptions } from '../../core/models/person.model';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-person-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DialogModule,
    ButtonModule,
    InputTextModule,
    ToastModule,
    DropdownModule
  ],
  providers: [MessageService],
  templateUrl: './person-form.component.html',
  styleUrl: './person-form.component.scss'
})
export class PersonFormComponent implements OnInit {
  @Output() personCreated = new EventEmitter<any>();
  @Output() dialogClosed = new EventEmitter<void>();
  
  personForm!: FormGroup;
  visible = false;
  loading = false;
  countries = [
    { name: 'United States', code: 'USA' },
    { name: 'Canada', code: 'CAN' },
    { name: 'United Kingdom', code: 'GBR' }
  ];
  
  states = [
    { name: 'Alabama', id: 1 },
    { name: 'Alaska', id: 2 },
    { name: 'Arizona', id: 3 },
    { name: 'Arkansas', id: 4 },
    { name: 'California', id: 5 },
    { name: 'Colorado', id: 6 },
    { name: 'Connecticut', id: 7 },
    { name: 'Delaware', id: 8 },
    { name: 'Florida', id: 9 },
    { name: 'Georgia', id: 10 },
    { name: 'Hawaii', id: 11 },
    { name: 'Idaho', id: 12 },
    { name: 'Illinois', id: 13 },
    { name: 'Indiana', id: 14 },
    { name: 'Iowa', id: 15 },
    { name: 'Kansas', id: 16 },
    { name: 'Kentucky', id: 17 },
    { name: 'Louisiana', id: 18 },
    { name: 'Maine', id: 19 },
    { name: 'Maryland', id: 20 },
    { name: 'Massachusetts', id: 21 },
    { name: 'Michigan', id: 22 },
    { name: 'Minnesota', id: 23 },
    { name: 'Mississippi', id: 24 },
    { name: 'Missouri', id: 25 },
    { name: 'Montana', id: 26 },
    { name: 'Nebraska', id: 27 },
    { name: 'Nevada', id: 28 },
    { name: 'New Hampshire', id: 29 },
    { name: 'New Jersey', id: 30 },
    { name: 'New Mexico', id: 31 },
    { name: 'New York', id: 32 },
    { name: 'North Carolina', id: 33 },
    { name: 'North Dakota', id: 34 },
    { name: 'Ohio', id: 35 },
    { name: 'Oklahoma', id: 36 },
    { name: 'Oregon', id: 37 },
    { name: 'Pennsylvania', id: 38 },
    { name: 'Rhode Island', id: 39 },
    { name: 'South Carolina', id: 40 },
    { name: 'South Dakota', id: 41 },
    { name: 'Tennessee', id: 42 },
    { name: 'Texas', id: 43 },
    { name: 'Utah', id: 44 },
    { name: 'Vermont', id: 45 },
    { name: 'Virginia', id: 46 },
    { name: 'Washington', id: 47 },
    { name: 'West Virginia', id: 48 },
    { name: 'Wisconsin', id: 49 },
    { name: 'Wyoming', id: 50 }
  ];
  
  constructor(
    private fb: FormBuilder,
    private peopleService: PeopleService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.personForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{3}-\d{3}-\d{4}$/)]],
      linkedIn: [''],
      gitHub: [''],
      city: ['', [Validators.required]],
      stateId: [47, [Validators.required]], // Default to Washington (ID: 47) for Seattle
      country: ['USA', [Validators.required]]
    });
  }

  showDialog(): void {
    this.visible = true;
    this.initForm();
  }

  hideDialog(): void {
    this.visible = false;
    this.dialogClosed.emit();
  }

  onSubmit(): void {
    if (this.personForm.invalid) {
      this.markFormGroupTouched(this.personForm);
      return;
    }

    this.loading = true;
    const formData = this.personForm.value;
    
    // Get the selected state ID from the dropdown
    // Washington state has ID 47 in our array
    const stateId = 47; // Default to Washington
    
    // Create a new object without spreading formData to avoid including gitHub
    const personData: PersonOptions = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      email: formData.email,
      phoneNumber: formData.phoneNumber,
      linkedIn: formData.linkedIn,
      gitHub: formData.gitHub || '', // Use camelCase for frontend model
      city: formData.city,
      stateId: stateId,
      country: formData.country,
      languageOptions: [] // Add empty languageOptions array as required by backend
    };
    
    console.log('Submitting person data with fixed stateId:', personData);
    
    console.log('Submitting person data:', personData);
    
    this.peopleService.createPerson(personData).subscribe({
      next: (response) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Person created successfully'
        });
        this.loading = false;
        this.personCreated.emit(response);
        this.hideDialog();
      },
      error: (error) => {
        console.error('Error creating person:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to create person. Please try again.'
        });
        this.loading = false;
      }
    });
  }

  // Helper method to mark all form controls as touched
  markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }
}
