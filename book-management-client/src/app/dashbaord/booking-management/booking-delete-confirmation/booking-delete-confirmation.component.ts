import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject, takeUntil, lastValueFrom, of, catchError, finalize } from 'rxjs';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BookingService } from '../../../shared/services/booking.service';
import { IBooking } from '../../../shared/interfaces/booking.interface';

@Component({
  selector: 'app-booking-delete-confirmation',
  templateUrl: './booking-delete-confirmation.component.html',
  styleUrls: ['./booking-delete-confirmation.component.scss']
})
export class BookingDeleteConfirmationComponent implements OnInit, OnDestroy {

  @BlockUI() ngBlockUi!: NgBlockUI;
  destroy$ = new Subject<boolean>();

  constructor(
    public matDialogRef: MatDialogRef<BookingDeleteConfirmationComponent>,
    private bookingService: BookingService,
    @Inject(MAT_DIALOG_DATA) public data: IBooking
  ) { }

  ngOnInit(): void {

  }

  onConfirmation = async (): Promise<void> => {
    this.ngBlockUi.start('Processing...');
    try {
      const isDeleted: boolean = await lastValueFrom(this.bookingService.deleteBookingAsync(this.data.uniqueId).pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          return of(error)
        }),
        finalize(() => {
          setTimeout(() => {
            this.ngBlockUi.stop();
          }, 2000);
        })));

      // TODO : SHOW TOASTER USING THIS VARIABLE [isDeleted]
      this.bookingService.refreshData.emit(true);
      this.onDialogClose();
    } catch (error) {
      console.log(error);
    }
  }

  onRejection = (): void => {
    this.onDialogClose()
  }

  onDialogClose = (): void => {
    this.matDialogRef.close();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
