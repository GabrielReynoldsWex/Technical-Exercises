import { CustomerFormComponent } from './customer-form/customer-form.component';
import { CustomerListComponent } from './customer-list/customer-list.component';

import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', component: CustomerFormComponent },
    { path: 'customer-list', component: CustomerListComponent },
];
