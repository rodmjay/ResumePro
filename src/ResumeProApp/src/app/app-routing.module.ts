import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PeopleComponent } from './people/people.component';
import { PersonDetailComponent } from './person-detail/person-detail.component';
import { ResumeDetailComponent } from './resume-detail/resume-detail.component';
import { ResumeCreateComponent } from './resume-create/resume-create.component';
import { SkillsComponent } from './skills/skills.component';
import { DashboardComponent } from './dashboard/dashboard.component';

export const routes: Routes = [
  { path: 'resumes/create', component: ResumeCreateComponent },
  { path: '', component: DashboardComponent, children: [
    { path: '', redirectTo: 'people', pathMatch: 'full' },
    { path: 'people', component: PeopleComponent },
    { path: 'people/:personId', component: PersonDetailComponent },
    { path: 'people/:personId/resume/:resumeId', component: ResumeDetailComponent },
    { path: 'people/:personId/resumes/create', component: ResumeCreateComponent },
    { path: 'skills', component: SkillsComponent }
  ]},
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
