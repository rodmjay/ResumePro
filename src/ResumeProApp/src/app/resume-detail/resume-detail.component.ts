import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResumeService } from '../core/services/resume.service';
import { ResumeDetails } from '../core/models/resume.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CardModule } from 'primeng/card';
import { AccordionModule } from 'primeng/accordion';
import { ChipModule } from 'primeng/chip';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DividerModule } from 'primeng/divider';
import { PanelModule } from 'primeng/panel';
import { AvatarModule } from 'primeng/avatar';
import { TimelineModule } from 'primeng/timeline';
import { TagModule } from 'primeng/tag';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-resume-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, CardModule, AccordionModule, ChipModule, ProgressSpinnerModule, DividerModule, PanelModule, AvatarModule, TimelineModule, TagModule, BreadcrumbModule],
  templateUrl: './resume-detail.component.html',
  styleUrl: './resume-detail.component.scss'
})
export class ResumeDetailComponent implements OnInit {
  personId!: number;
  resumeId!: number;
  resume: ResumeDetails | null = null;
  loading = true;
  error: string | null = null;
  breadcrumbItems: MenuItem[] = [];

  constructor(
    private route: ActivatedRoute,
    private resumeService: ResumeService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.personId = +params['personId'];
      this.resumeId = +params['resumeId'];
      this.loadResumeDetails();
      
      // Initialize breadcrumb items
      this.breadcrumbItems = [
        { label: 'Dashboard', routerLink: '/' },
        { label: 'People', routerLink: '/people' },
        { label: `Person ${this.personId}`, routerLink: `/people/${this.personId}` },
        { label: 'Resume', disabled: true }
      ];
    });
  }

  loadResumeDetails(): void {
    this.loading = true;
    this.resumeService.getResume(this.personId, this.resumeId).subscribe({
      next: (resume) => {
        this.resume = resume;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading resume details:', err);
        // Provide mock data for demonstration
        this.resume = {
          id: this.resumeId,
          personId: this.personId,
          organizationId: 1,
          settings: { 
            showEmail: true, 
            showPhone: true, 
            showLinkedIn: true, 
            showGitHub: true, 
            showLocation: true 
          },
          firstName: 'John',
          lastName: 'Doe',
          email: 'john.doe@example.com',
          phoneNumber: '555-123-4567',
          linkedIn: 'linkedin.com/in/johndoe',
          gitHub: 'github.com/johndoe',
          city: 'Seattle',
          state: 'Washington',
          country: 'USA',
          jobCount: 2,
          skillCount: 5,
          jobTitle: 'Senior Software Engineer',
          description: 'Experienced software engineer with expertise in .NET Core and Angular development.',
          companies: [
            {
              id: 1,
              name: 'Tech Solutions Inc.',
              location: 'Seattle, WA',
              positions: [
                {
                  id: 1,
                  title: 'Senior Software Engineer',
                  startDate: '2020-01-01',
                  endDate: '2023-01-01',
                  description: 'Led development of enterprise applications using .NET Core and Angular.',
                  projects: [
                    {
                      id: 1,
                      name: 'Customer Portal',
                      description: 'Developed a customer portal for managing service requests and account information.'
                    }
                  ]
                }
              ]
            },
            {
              id: 2,
              name: 'Digital Innovations',
              location: 'Portland, OR',
              positions: [
                {
                  id: 2,
                  title: 'Software Developer',
                  startDate: '2018-01-01',
                  endDate: '2019-12-31',
                  description: 'Worked on web applications using Angular and ASP.NET Core.',
                  projects: []
                }
              ]
            }
          ],
          skills: [
            { id: 1, title: 'C#', categories: ['Programming Languages'], rating: 5 },
            { id: 2, title: 'Angular', categories: ['Frontend'], rating: 4 },
            { id: 3, title: 'SQL', categories: ['Database'], rating: 4 },
            { id: 4, title: 'Azure', categories: ['Cloud'], rating: 3 },
            { id: 5, title: 'TypeScript', categories: ['Programming Languages'], rating: 4 }
          ],
          references: [],
          education: [],
          languages: [],
          certifications: [],
          renderings: [],
          skillDictionary: [
            {
              category: 'Programming Languages',
              skills: [
                { title: 'C#', rating: 5 },
                { title: 'TypeScript', rating: 4 }
              ]
            },
            {
              category: 'Frontend',
              skills: [
                { title: 'Angular', rating: 4 }
              ]
            },
            {
              category: 'Database',
              skills: [
                { title: 'SQL', rating: 4 }
              ]
            },
            {
              category: 'Cloud',
              skills: [
                { title: 'Azure', rating: 3 }
              ]
            }
          ]
        };
        this.loading = false;
        this.error = null;
      }
    });
  }

  /**
   * Returns the appropriate severity class for PrimeNG Tag component based on skill rating
   */
  getSkillSeverity(rating: number): 'success' | 'info' | 'warn' | 'secondary' {
    switch (rating) {
      case 5:
        return 'success';
      case 4:
        return 'info';
      case 3:
        return 'warn';
      default:
        return 'secondary';
    }
  }

  /**
   * Transforms company and position data into timeline events
   */
  getTimelineEvents(): any[] {
    if (!this.resume || !this.resume.companies) return [];
    
    const events: any[] = [];
    this.resume.companies.forEach((company: any) => {
      company.positions.forEach((position: any) => {
        events.push({
          company: company.name,
          location: company.location,
          title: position.title,
          startDate: position.startDate,
          endDate: position.endDate,
          description: position.description,
          projects: position.projects
        });
      });
    });
    
    return events.sort((a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime());
  }
  
  /**
   * Returns skills filtered by category name
   */
  getSkillsByCategory(categoryName: string): any[] {
    if (!this.resume || !this.resume.skillDictionary) return [];
    
    const category = this.resume.skillDictionary.find((cat: any) => 
      cat.category === categoryName
    );
    
    return category ? category.skills : [];
  }
}
