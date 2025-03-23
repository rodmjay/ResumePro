import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AppMenuitem as AppMenuitemComponent } from './app.menuitem';

@Component({
    selector: 'app-menu',
    standalone: true,
    imports: [CommonModule, AppMenuitemComponent, RouterModule],
    template: `<ul class="layout-menu">
        <ng-container *ngFor="let item of model; let i = index">
            <li
                app-menuitem
                *ngIf="!item.separator"
                [item]="item"
                [index]="i"
                [root]="true"
            ></li>
            <li *ngIf="item.separator" class="menu-separator"></li>
        </ng-container>
    </ul> `,
})
export class AppMenu {
    model: any[] = [];

    ngOnInit() {
        this.model = [
            {
                label: 'ResumePro',
                icon: 'pi pi-home',
                items: [
                    {
                        label: 'Dashboard',
                        icon: 'pi pi-fw pi-home',
                        routerLink: ['/'],
                    },
                    {
                        label: 'People',
                        icon: 'pi pi-fw pi-users',
                        routerLink: ['/people'],
                    },
                ],
            },
            {
                label: 'Skills',
                icon: 'pi pi-star',
                items: [
                    {
                        label: 'Manage Skills',
                        icon: 'pi pi-fw pi-list',
                        routerLink: ['/skills'],
                    },
                    {
                        label: 'Add Skill',
                        icon: 'pi pi-fw pi-plus',
                        routerLink: ['/skills/add'],
                    },
                ],
            },
            {
                label: 'Resumes',
                icon: 'pi pi-fw pi-file',
                items: [
                    {
                        label: 'My Resumes',
                        icon: 'pi pi-fw pi-list',
                        routerLink: ['/resumes'],
                    },
                    {
                        label: 'Create Resume',
                        icon: 'pi pi-fw pi-plus',
                        routerLink: ['/resumes/create'],
                    },
                ],
            },
            {
                label: 'Settings',
                icon: 'pi pi-fw pi-cog',
                items: [
                    {
                        label: 'Profile',
                        icon: 'pi pi-fw pi-user',
                        routerLink: ['/settings/profile'],
                    },
                    {
                        label: 'Preferences',
                        icon: 'pi pi-fw pi-sliders-h',
                        routerLink: ['/settings/preferences'],
                    },
                ],
            },
        ];
    }
}
