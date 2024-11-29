

import { Routes } from '@angular/router';

export const routes: Routes = [
 
    { path: '', loadComponent: () => import('./pages/patient-page/patient-page.component').then(m => m.PatientPageComponent) },
    
];
