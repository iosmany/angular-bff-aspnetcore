import { Component, Input } from "@angular/core";
import { MatIcon } from "@angular/material/icon";
import { MatToolbar } from "@angular/material/toolbar";



@Component({    
    selector: 'app-page-header',
    imports: [
        MatToolbar,
        MatIcon
    ],
    template: `<!-- Header -->
    <mat-toolbar color="primary">
      <button mat-icon-button>
        <mat-icon>menu</mat-icon>
      </button>
      <span>{{ title }}</span>
    </mat-toolbar>`,
})
export class PageHeaderComponent {
    @Input({ required: true }) title = 'Dashboard';
}