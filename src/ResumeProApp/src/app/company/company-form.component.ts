import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextarea } from 'primeng/inputtextarea';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { CalendarModule } from 'primeng/calendar';
import { CompanyService } from '../core/services/company.service';
import { CompanyDetails, CompanyOptions } from '../core/models/company.model';

@Component({
  selector: 'app-company-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DialogModule,
    ButtonModule,
    InputTextModule,
    InputTextarea,
    CalendarModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './company-form.component.html',
  styleUrl: './company-form.component.scss'
})
export class CompanyFormComponent implements OnInit {
  @Input() personId!: number;
  @Input() company: CompanyDetails | null = null;
  @Output() companyCreated = new EventEmitter<CompanyDetails>();
  @Output() companyUpdated = new EventEmitter<CompanyDetails>();
  @Output() dialogClosed = new EventEmitter<void>();
  
  companyForm!: FormGroup;
  visible = false;
  loading = false;
  isEditMode = false;
  
  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.companyForm = this.fb.group({
      company: [this.company?.company || '', [Validators.required, Validators.maxLength(100)]],
      description: [this.company?.description || '', [Validators.maxLength(500)]],
      location: [this.company?.location || '', [Validators.required, Validators.maxLength(100)]],
      startDate: [this.company?.startDate || '', [Validators.required]],
      endDate: [this.company?.endDate || '']
    });
  }

  showDialog(company?: CompanyDetails): void {
    this.isEditMode = !!company;
    this.company = company || null;
    this.initForm();
    this.visible = true;
  }

  hideDialog(): void {
    this.visible = false;
    this.dialogClosed.emit();
  }

  onSubmit(): void {
    if (this.companyForm.invalid) {
      this.markFormGroupTouched(this.companyForm);
      return;
    }

    this.loading = true;
    const companyData: CompanyOptions = this.companyForm.value;
    
    if (this.isEditMode && this.company) {
      this.companyService.updateCompany(this.personId, this.company.id, companyData).subscribe({
        next: (response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Company updated successfully'
          });
          this.loading = false;
          this.companyUpdated.emit(response);
          this.hideDialog();
        },
        error: (error) => {
          console.error('Error updating company:', error);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to update company. Please try again.'
          });
          this.loading = false;
        }
      });
    } else {
      this.companyService.createCompany(this.personId, companyData).subscribe({
        next: (response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Company created successfully'
          });
          this.loading = false;
          this.companyCreated.emit(response);
          this.hideDialog();
        },
        error: (error) => {
          console.error('Error creating company:', error);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to create company. Please try again.'
          });
          this.loading = false;
        }
      });
    }
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
