import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CardModule } from 'primeng/card';
import { TabViewModule } from 'primeng/tabview';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ChipModule } from 'primeng/chip';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessageService, ConfirmationService } from 'primeng/api';
import { CompanyService } from '../core/services/company.service';
import { CompanyDetails } from '../core/models/company.model';
import { CompanyFormComponent } from './company-form.component';

@Component({
  selector: 'app-company-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    CardModule,
    TabViewModule,
    ButtonModule,
    TableModule,
    ChipModule,
    ProgressSpinnerModule,
    ToastModule,
    ConfirmDialogModule,
    CompanyFormComponent
  ],
  providers: [MessageService, ConfirmationService],
  templateUrl: './company-detail.component.html',
  styleUrl: './company-detail.component.scss'
})
export class CompanyDetailComponent implements OnInit {
  @ViewChild('companyForm') companyForm!: CompanyFormComponent;
  
  personId!: number;
  companyId!: number;
  company: CompanyDetails | null = null;
  loading = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private companyService: CompanyService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.personId = +params['personId'];
      this.companyId = +params['companyId'];
      this.loadCompanyDetails();
    });
  }

  loadCompanyDetails(): void {
    this.loading = true;
    this.companyService.getCompany(this.personId, this.companyId).subscribe({
      next: (company) => {
        this.company = company;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading company details:', err);
        this.error = 'Failed to load company details. Please try again.';
        this.loading = false;
        
        // Provide mock data for demonstration
        this.company = {
          id: this.companyId,
          name: this.companyId === 1 ? 'Microsoft' : 'Google',
          description: this.companyId === 1 ? 'Global technology company' : 'Search and advertising company',
          website: this.companyId === 1 ? 'https://microsoft.com' : 'https://google.com',
          positions: [
            {
              id: 1,
              title: this.companyId === 1 ? 'Software Engineer' : 'Frontend Developer',
              startDate: '2018-01-01',
              endDate: '2020-12-31',
              description: this.companyId === 1 ? 'Developed cloud services' : 'Worked on Google Maps',
              projects: [
                {
                  id: 1,
                  name: this.companyId === 1 ? 'Azure Functions' : 'Maps UI Redesign',
                  description: this.companyId === 1 ? 'Serverless computing service' : 'Redesigned the UI for Google Maps',
                  highlights: [
                    { id: 1, text: 'Implemented new features' },
                    { id: 2, text: 'Improved performance by 30%' }
                  ]
                }
              ]
            }
          ],
          skills: [
            { id: 1, title: this.companyId === 1 ? 'C#' : 'JavaScript' },
            { id: 2, title: this.companyId === 1 ? 'Azure' : 'Angular' }
          ]
        };
      }
    });
  }

  showEditCompanyDialog(): void {
    if (this.company) {
      this.companyForm.showDialog(this.company);
    }
  }

  onCompanyUpdated(company: CompanyDetails): void {
    this.company = company;
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Company information updated successfully'
    });
  }

  deleteCompany(): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this company?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.companyService.deleteCompany(this.personId, this.companyId).subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Company deleted successfully'
            });
            this.router.navigate(['/people', this.personId, 'companies']);
          },
          error: (error) => {
            console.error('Error deleting company:', error);
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete company'
            });
          }
        });
      }
    });
  }

  addPosition(): void {
    // This would navigate to position form or open a dialog
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Add Position functionality will be implemented in the next phase'
    });
  }

  editPosition(positionId: number): void {
    // This would navigate to position form or open a dialog
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Edit Position functionality will be implemented in the next phase'
    });
  }

  deletePosition(positionId: number): void {
    // This would confirm and delete the position
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Delete Position functionality will be implemented in the next phase'
    });
  }

  addSkill(): void {
    // This would navigate to skill selection or open a dialog
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Add Skill functionality will be implemented in the next phase'
    });
  }

  removeSkill(skillId: number): void {
    // This would confirm and remove the skill
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Remove Skill functionality will be implemented in the next phase'
    });
  }

  goBack(): void {
    this.router.navigate(['/people', this.personId, 'companies']);
  }
}
