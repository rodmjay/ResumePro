import { Component, OnInit, ViewChild } from '@angular/core';
import { PeopleService } from '../core/services/people.service';
import { PagedList, PagingQuery } from '../core/models/base.model';
import { PersonaDto, PersonaFilters, PersonaDetails } from '../core/models/person.model';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { PersonFormComponent } from './person-form/person-form.component';

@Component({
  selector: 'app-people',
  standalone: true,
  imports: [
    CommonModule, 
    RouterModule, 
    TableModule, 
    ButtonModule, 
    ProgressSpinnerModule,
    ToastModule,
    PersonFormComponent
  ],
  providers: [MessageService],
  templateUrl: './people.component.html',
  styleUrl: './people.component.scss'
})
export class PeopleComponent implements OnInit {
  @ViewChild('personForm') personForm!: PersonFormComponent;
  
  people: PersonaDto[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private peopleService: PeopleService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.loadPeople();
  }
  
  navigateToPerson(person: PersonaDto): void {
    console.log('Navigating to person:', person.id);
    this.router.navigate(['/people', person.id]);
  }

  showCreatePersonDialog(): void {
    this.personForm.showDialog();
  }

  onPersonCreated(person: PersonaDetails): void {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: `Person ${person.firstName} ${person.lastName} created successfully`
    });
    this.loadPeople();
  }

  loadPeople(): void {
    this.loading = true;
    this.peopleService.getPeople().subscribe({
      next: (response) => {
        this.people = response.items || [];
        this.loading = false;
        console.log('People loaded:', this.people);
      },
      error: (err) => {
        this.error = 'Failed to load people data. Please make sure the API is running.';
        this.loading = false;
        console.error('Error loading people:', err);
      }
    });
  }
}
