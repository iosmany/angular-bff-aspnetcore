import { Component } from '@angular/core';
import { MatToolbar } from '@angular/material/toolbar';

@Component({
  selector: 'app-footer',
  imports: [
    MatToolbar
  ],
  template: `
  <!-- Footer -->
    <mat-toolbar color="accent">
      <span>&copy; 2024 Your Company</span>
    </mat-toolbar>`
})
export class FooterComponent {

}
