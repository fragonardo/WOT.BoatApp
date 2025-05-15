import { Routes } from '@angular/router';
import { HomeComponent } from './views/home/home.component';
import { BoatsListComponent } from './views/boats/boats-list/boats-list.component';
import { BoatsDetailComponent } from './views/boats/boats-detail/boats-detail.component';

export const routes: Routes = [    
    { path: 'boats', component: BoatsListComponent, pathMatch: 'full' },
    { path: 'boats/:id', component: BoatsDetailComponent },
    { path: '', redirectTo: 'boats', pathMatch: 'full' },
];
