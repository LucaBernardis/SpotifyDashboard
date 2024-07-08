import { Routes } from '@angular/router';
import { DashboardWrapperComponent } from './components/dashboard-wrapper/dashboard-wrapper.component';

export const routes: Routes = [
    
    {path: '**', component: DashboardWrapperComponent}

];
