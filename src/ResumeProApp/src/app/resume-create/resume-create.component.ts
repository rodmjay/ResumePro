import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ResumeService } from '../core/services/resume.service';
import { PeopleService } from '../core/services/people.service';
import { ResumeOptions } from '../core/models/resume.model';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-resume-create',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    CheckboxModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './resume-create.component.html',
  styleUrl: './resume-create.component.scss'
})
export class ResumeCreateComponent implements OnInit {
  resumeForm!: FormGroup;
  personId!: number;
  personName: string = '';
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private resumeService: ResumeService,
    private peopleService: PeopleService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.personId = +params['personId'];
      this.initForm();
      this.loadPersonDetails();
    });
  }
  
  loadPersonDetails(): void {
    if (this.personId) {
      // Try to load person details from API
      this.peopleService.getPerson(this.personId).subscribe({
        next: (person) => {
          this.personName = `${person.firstName} ${person.lastName}`;
        },
        error: (error) => {
          console.error('Error loading person details:', error);
          // Fallback to mock data since API is not available
          if (this.personId === 1) {
            this.personName = 'John Doe';
          } else if (this.personId === 2) {
            this.personName = 'Jane Smith';
          } else {
            this.personName = `Person ${this.personId}`;
          }
        }
      });
    }
  }

  initForm(): void {
    this.resumeForm = this.formBuilder.group({
      jobTitle: ['', Validators.required],
      description: ['', Validators.required],
      skillIds: [[]],
      companyIds: [[]],
      settings: this.formBuilder.group({
        showEmail: [true],
        showPhone: [true],
        showLinkedIn: [true],
        showGitHub: [true],
        showLocation: [true]
      })
    });
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.resumeForm.invalid) {
      return;
    }

    this.loading = true;
    const resumeOptions: ResumeOptions = this.resumeForm.value;

    this.resumeService.createResume(this.personId, resumeOptions).subscribe({
      next: (resume) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Resume created successfully'
        });
        this.loading = false;
        // Navigate to the newly created resume
        this.router.navigate(['/people', this.personId, 'resume', resume.id]);
      },
      error: (error) => {
        console.error('Error creating resume:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to create resume. Please try again.'
        });
        this.loading = false;
      }
    });
  }

  get f() {
    return this.resumeForm.controls;
  }
}
