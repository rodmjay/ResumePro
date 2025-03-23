import { Routes } from '@angular/router';
import { AppLayout } from './app/layout/components/app.layout';
// Temporarily comment out unused imports
// import { Landing } from '@/pages/landing/landing';
// import { Notfound } from '@/pages/notfound/notfound';

export const appRoutes: Routes = [
    {
        path: '',
        component: AppLayout,
        children: [
            {
                path: '',
                loadComponent: () => import('./app/dashboard/dashboard.component').then(c => c.DashboardComponent),
                data: { breadcrumb: 'Dashboard' },
            },
            {
                path: 'people',
                loadComponent: () => import('./app/people/people.component').then(c => c.PeopleComponent),
                data: { breadcrumb: 'People' },
            },
            {
                path: 'people/:personId',
                loadComponent: () => import('./app/person-detail/person-detail.component').then(c => c.PersonDetailComponent),
                data: { breadcrumb: 'Person Details' },
            },
            {
                path: 'people/:personId/resume/:resumeId',
                loadComponent: () => import('./app/resume-detail/resume-detail.component').then(c => c.ResumeDetailComponent),
                data: { breadcrumb: 'Resume Details' },
            },
            {
                path: 'resumes',
                loadComponent: () => import('./app/dashboard/dashboard.component').then(c => c.DashboardComponent),
                data: { breadcrumb: 'My Resumes' },
            },
            {
                path: 'resumes/create',
                loadComponent: () => import('./app/resume-create/resume-create.component').then(c => c.ResumeCreateComponent),
                data: { breadcrumb: 'Create Resume' },
            },
            {
                path: 'people/:personId/resumes/create',
                loadComponent: () => import('./app/resume-create/resume-create.component').then(c => c.ResumeCreateComponent),
                data: { breadcrumb: 'Create Resume' },
            },
            {
                path: 'skills',
                loadComponent: () => import('./app/skills/skills.component').then(c => c.SkillsComponent),
                data: { breadcrumb: 'Skills' },
            },
        ],
    },
    { path: '**', redirectTo: '' },
];
