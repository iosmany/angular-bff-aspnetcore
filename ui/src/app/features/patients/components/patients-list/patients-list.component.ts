import { Component, OnInit } from '@angular/core';
import { PatientsService } from '../../patients.service';
import { Patient } from '../../patients.models';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-patients-list',
  imports: [
    MatTableModule,
  ],
  templateUrl: './patients-list.component.html',
  styleUrl: './patients-list.component.scss'
})
export class PatientsListComponent implements OnInit {

  constructor(private patientsService: PatientsService) {
  }

  patients: Patient[] = [];  
  displayedColumns: string[] = ['name', 'age', 'actions'];

  ngOnInit(): void {
    this.patientsService.getPatients().subscribe(patients => {
      if (patients.error) {
        console.error(patients.error);
        return;
      }
      this.patients = patients.data;
    });
  }

  edit(patient: Patient) {
    console.log(patient);
  }

  delete(patient: Patient) {
    console.log(patient);
  }

}
