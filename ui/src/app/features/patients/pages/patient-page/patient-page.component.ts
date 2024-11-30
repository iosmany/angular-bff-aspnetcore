import { Component } from '@angular/core';
import { AuthorizationComponent } from '../../../../shared/auth/authorization.component';
import { PatientsListComponent } from '../../components/patients-list/patients-list.component';

@Component({
  selector: 'app-patient-page',
  imports: [
    PatientsListComponent
  ],
  templateUrl: './patient-page.component.html',
  styleUrl: './patient-page.component.scss'
})
export class PatientPageComponent extends AuthorizationComponent {

}
