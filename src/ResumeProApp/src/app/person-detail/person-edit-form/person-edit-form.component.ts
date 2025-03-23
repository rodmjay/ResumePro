import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { PeopleService } from '../../core/services/people.service';
import { PersonOptions, PersonaDetails } from '../../core/models/person.model';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-person-edit-form',
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
  templateUrl: './person-edit-form.component.html',
  styleUrl: './person-edit-form.component.scss'
})
export class PersonEditFormComponent implements OnInit, OnChanges {
  @Input() person: PersonaDetails | null = null;
  @Input() personId!: number;
  @Output() personUpdated = new EventEmitter<PersonaDetails>();
  @Output() dialogClosed = new EventEmitter<void>();
  
  personForm!: FormGroup;
  visible = false;
  loading = false;
  countries = [
    { name: 'United States', code: 'USA' },
    { name: 'Canada', code: 'CAN' },
    { name: 'United Kingdom', code: 'GBR' }
  ];
  
  constructor(
    private fb: FormBuilder,
    private peopleService: PeopleService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['person'] && this.person) {
      this.initForm();
    }
  }

  initForm(): void {
    this.personForm = this.fb.group({
      firstName: [this.person?.firstName || '', [Validators.required, Validators.maxLength(50)]],
      lastName: [this.person?.lastName || '', [Validators.required, Validators.maxLength(50)]],
      email: [this.person?.email || '', [Validators.required, Validators.email, Validators.maxLength(100)]],
      phoneNumber: [this.person?.phoneNumber || '', [Validators.required, Validators.pattern(/^\d{3}-\d{3}-\d{4}$/)]],
      linkedIn: [this.person?.linkedIn || ''],
      gitHub: [this.person?.gitHub || ''],
      city: [this.person?.city || '', [Validators.required]],
      state: [this.person?.state || '', [Validators.required]],
      country: [this.person?.country || 'USA', [Validators.required]]
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
    const personData: PersonOptions = this.personForm.value;
    
    this.peopleService.updatePerson(this.personId, personData).subscribe({
      next: (response) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Person updated successfully'
        });
        this.loading = false;
        this.personUpdated.emit(response);
        this.hideDialog();
      },
      error: (error) => {
        console.error('Error updating person:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to update person. Please try again.'
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
