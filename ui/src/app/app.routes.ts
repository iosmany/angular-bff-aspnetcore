import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';

export const routes: Routes = [
 
    { path: '', component: HomeComponent },
    { path: 'home', redirectTo: '', pathMatch: 'full' },
    { path: 'dashboard', redirectTo: '', pathMatch: 'full' },

    { path: 'patients', loadChildren: () => import('./features/patients/patients.routes').then(m => m.routes) },

    
];
