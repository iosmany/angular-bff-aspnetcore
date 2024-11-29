import { Component } from '@angular/core';
import { AuthorizationComponent } from '../../../../shared/auth/authorization.component';

@Component({
  selector: 'app-patient-page',
  imports: [],
  templateUrl: './patient-page.component.html',
  styleUrl: './patient-page.component.scss'
})
export class PatientPageComponent extends AuthorizationComponent {

}
