import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';

import { BookingManagementComponent } from './booking-management.component';
import { BookingDetailsComponent } from './booking-details/booking-details.component';

const routes: Routes = [
  {
    path: 'details', component: BookingManagementComponent, children:
      [
        { path: '', component: BookingDetailsComponent, pathMatch: 'full' },
        { path: '**', redirectTo: '' },
      ],
    pathMatch: 'full'
  },
  { path: '**', redirectTo: 'details' }
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookingRoutingModule { }
