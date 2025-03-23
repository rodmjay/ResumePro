import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessageService, ConfirmationService } from 'primeng/api';
import { CompanyService } from '../core/services/company.service';
import { CompanyDetails } from '../core/models/company.model';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    TableModule,
    ButtonModule,
    ProgressSpinnerModule,
    ToastModule,
    ConfirmDialogModule
  ],
  providers: [MessageService, ConfirmationService],
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.scss'
})
export class CompanyListComponent implements OnInit {
  companies: CompanyDetails[] = [];
  loading = true;
  error: string | null = null;
  personId!: number;

  constructor(
    private companyService: CompanyService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    // Get personId from route params
    const urlParts = this.router.url.split('/');
    const personIdIndex = urlParts.indexOf('people') + 1;
    if (personIdIndex > 0 && personIdIndex < urlParts.length) {
      this.personId = +urlParts[personIdIndex];
      this.loadCompanies();
    } else {
      this.error = 'Invalid route. Person ID not found.';
      this.loading = false;
    }
  }

  loadCompanies(): void {
    this.loading = true;
    this.companyService.getCompanies(this.personId).subscribe({
      next: (companies) => {
        this.companies = companies;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading companies:', err);
        this.error = 'Failed to load companies. Please try again.';
        this.loading = false;
        
        // Provide mock data for demonstration
        this.companies = [
          {
            id: 1,
            name: 'Microsoft',
            description: 'Global technology company',
            website: 'https://microsoft.com',
            positions: [
              {
                id: 1,
                title: 'Software Engineer',
                startDate: '2018-01-01',
                endDate: '2020-12-31',
                description: 'Developed cloud services',
                projects: []
              }
            ],
            skills: [
              { id: 1, title: 'C#' },
              { id: 2, title: 'Azure' }
            ]
          },
          {
            id: 2,
            name: 'Google',
            description: 'Search and advertising company',
            website: 'https://google.com',
            positions: [
              {
                id: 2,
                title: 'Frontend Developer',
                startDate: '2016-03-01',
                endDate: '2017-12-31',
                description: 'Worked on Google Maps',
                projects: []
              }
            ],
            skills: [
              { id: 3, title: 'JavaScript' },
              { id: 4, title: 'Angular' }
            ]
          }
        ];
      }
    });
  }

  createCompany(): void {
    this.router.navigate(['/people', this.personId, 'companies', 'new']);
  }

  viewCompany(companyId: number): void {
    this.router.navigate(['/people', this.personId, 'companies', companyId]);
  }

  deleteCompany(companyId: number): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this company?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.companyService.deleteCompany(this.personId, companyId).subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Company deleted successfully'
            });
            this.loadCompanies();
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
}
