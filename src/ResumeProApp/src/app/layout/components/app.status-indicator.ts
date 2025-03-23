import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AppState } from '../../core/state';
import { selectIsApiChecking, selectIsApiOnline } from '../../core/state/api/api.selectors';

@Component({
  selector: 'app-status-indicator',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="api-status-indicator">
      <div class="status-dot" [ngClass]="statusClass$ | async"></div>
      <span class="status-text">API: {{ statusText$ | async }}</span>
    </div>
  `,
  styles: [`
    .api-status-indicator {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      padding: 0 1rem;
    }
    .status-dot {
      width: 10px;
      height: 10px;
      border-radius: 50%;
    }
    .status-up {
      background-color: #22c55e;
    }
    .status-down {
      background-color: #ef4444;
    }
    .status-checking {
      background-color: #f59e0b;
    }
    .status-text {
      font-size: 0.875rem;
      color: var(--text-color-secondary);
    }
  `]
})
export class AppStatusIndicator implements OnInit {
  isOnline$: Observable<boolean>;
  isChecking$: Observable<boolean>;
  statusClass$!: Observable<string>;
  statusText$!: Observable<string>;

  constructor(private store: Store<AppState>) {
    this.isOnline$ = this.store.select(selectIsApiOnline);
    this.isChecking$ = this.store.select(selectIsApiChecking);
  }

  ngOnInit(): void {
    this.statusClass$ = this.store.select(state => {
      const apiState = state.api;
      if (apiState.isChecking) return 'status-checking';
      return apiState.isOnline ? 'status-up' : 'status-down';
    });

    this.statusText$ = this.store.select(state => {
      const apiState = state.api;
      if (apiState.isChecking) return 'Checking';
      return apiState.isOnline ? 'Online' : 'Offline';
    });
  }
}
