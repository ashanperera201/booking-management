import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule, } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxPermissionsModule } from 'ngx-permissions';
import { MatSelectModule } from '@angular/material/select';

import { BookingManagementComponent } from './booking-management.component';
import { BookingRoutingModule } from './booking-management.routing';
import { BookingDetailsComponent } from './booking-details/booking-details.component';
import { BookingService } from '../../shared/services/booking.service';
import { BookingFormComponent } from './booking-form/booking-form.component';
import { BookingDeleteConfirmationComponent } from './booking-delete-confirmation/booking-delete-confirmation.component';

@NgModule({
  declarations: [
    BookingManagementComponent,
    BookingDetailsComponent,
    BookingFormComponent,
    BookingDeleteConfirmationComponent
  ],
  imports: [
    CommonModule,
    BookingRoutingModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxPermissionsModule,
    MatSelectModule
  ],
  providers: [BookingService]
})
export class BookingManagementModule { }
