import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';

import { BookingService } from '../../../shared/services/booking.service';
import { IBooking } from '../../../shared/interfaces/booking.interface';
import { BookingFormComponent } from '../booking-form/booking-form.component';
import { BookingDeleteConfirmationComponent } from '../booking-delete-confirmation/booking-delete-confirmation.component';


@Component({
  selector: 'app-booking-details',
  templateUrl: './booking-details.component.html',
  styleUrls: ['./booking-details.component.scss']
})
export class BookingDetailsComponent implements OnInit, OnDestroy {

  displayedColumns: string[] = ['id', 'fromAddress', 'toAddress', 'goodType', 'bookedTime', 'weight', 'pricing', 'isActive', 'createdBy', 'createdDateTime', 'actions'];
  dataSource!: MatTableDataSource<IBooking>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  destroy$ = new Subject<boolean>();

  pageSize: number = 5;
  page: number = 1;
  totalRecords: number = 0;


  constructor(private bookingService: BookingService, public matDialog: MatDialog) {
  }

  ngOnInit(): void {
    this.fetchAllBookingDetails();
    this.onRefresh();
  }

  onRefresh = (): void => {
    this.bookingService.refreshData.pipe(takeUntil(this.destroy$)).subscribe(res => {
      if (res) {
        this.fetchAllBookingDetails();
      }
    })
  }

  fetchAllBookingDetails = (): void => {
    this.bookingService.getAllBookingDetails(this.page, this.pageSize).pipe(takeUntil(this.destroy$)).subscribe({
      next: (serviceResponse) => {
        this.totalRecords = serviceResponse.total;
        this.dataSource = new MatTableDataSource(serviceResponse.records);
      },
      error: (e) => {
        console.log(e);
      }
    })
  }

  onBookingOpen = (bookDetail?: IBooking): void => {
    if (bookDetail) {
      this.matDialog.open(BookingFormComponent, {
        height: 'auto',
        width: '85%',
        data: { ...bookDetail }
      })
    } else {
      this.matDialog.open(BookingFormComponent, {
        height: 'auto',
        width: '85%',
      })
    }
  }

  onBookingDeleteOpen = (bookDetail?: IBooking): void => {
    if (bookDetail) {
      this.matDialog.open(BookingDeleteConfirmationComponent, {
        height: 'auto',
        width: '50%',
        data: { ...bookDetail }
      })
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
