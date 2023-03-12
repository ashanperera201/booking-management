import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { FormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject, takeUntil, lastValueFrom, of, catchError, finalize } from 'rxjs';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BookingService } from '../../../shared/services/booking.service';
import { IBooking } from '../../../shared/interfaces/booking.interface';
import { DriverService } from '../../../shared/services/driver.service';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.scss']
})
export class BookingFormComponent implements OnInit, OnDestroy {

  @BlockUI() ngBlockUi!: NgBlockUI;

  bookingForm!: FormGroup;
  destroy$ = new Subject<boolean>();
  isUpdate: boolean = false;
  driverList!: any[];

  constructor(public matDialogRef: MatDialogRef<BookingFormComponent>, private bookingService: BookingService, @Inject(MAT_DIALOG_DATA) public data: IBooking, private driverService: DriverService) { }

  ngOnInit(): void {
    this.fetchDriverDetails();
    this.initializeForm();
    this.patchForm();
  }

  fetchDriverDetails = (): void => {
    this.driverList = this.driverService.getDriverData();
  }

  initializeForm = (): void => {
    this.bookingForm = new FormGroup({
      fromAddress: new UntypedFormControl('', [Validators.required]),
      toAddress: new UntypedFormControl('', [Validators.required]),
      goodType: new UntypedFormControl('', [Validators.required]),
      weight: new UntypedFormControl('', [Validators.required]),
      pricing: new UntypedFormControl('', [Validators.required]),
      bookedTime: new UntypedFormControl('', [Validators.required]),
      assignedDriver: new UntypedFormControl('', [Validators.required]),
    });
  }

  patchForm = (): void => {
    if (this.data) {
      this.isUpdate = true;

      const patchValues = {
        fromAddress: this.data.fromAddress,
        toAddress: this.data.toAddress,
        goodType: this.data.goodType,
        weight: this.data.weight,
        pricing: this.data.pricing,
        bookedTime: this.data.bookedTime,
        assignedDriver: +this.data.assignedDriver,
      }

      this.bookingForm.patchValue(patchValues);
    }
  }

  onDialogClose = (): void => {
    this.matDialogRef.close();
  }

  onBookingProcess = async (): Promise<void> => {
    this.ngBlockUi.start('Processing....')
    Object.keys(this.bookingForm.controls).forEach(field => {
      const control = this.bookingForm.get(field);
      if (control) {
        control.markAsTouched({ onlySelf: true });
      }
    });

    if (this.bookingForm.valid) {
      try {
        if (this.isUpdate) {
          this.data.fromAddress = this.bookingForm.get('fromAddress')?.value ?? '';
          this.data.toAddress = this.bookingForm.get('toAddress')?.value ?? '';
          this.data.goodType = this.bookingForm.get('goodType')?.value ?? '';
          this.data.weight = +this.bookingForm.get('weight')?.value ?? 0;
          this.data.pricing = +this.bookingForm.get('pricing')?.value ?? 0;
          this.data.bookedTime = this.bookingForm.get('bookedTime')?.value ?? '';
          this.data.assignedDriver = this.bookingForm.get('assignedDriver')?.value?.toString() ?? '';
          this.data.isActive = true;
          this.data.updatedBy = '0';

          const udpatedResult = await lastValueFrom(this.bookingService.updateBookingAsync(this.data).pipe(
            takeUntil(this.destroy$),
            catchError(e => {
              return of(e);
            }),
            finalize(() => {
              this.onAssignment();
              const timeoutRef = setTimeout(() => {
                this.ngBlockUi.stop();
                clearTimeout(timeoutRef);
              }, 2000);
            })
          ));

          // TODO: SHOW TOAST.
          this.onDialogClose();
          this.bookingService.refreshData.emit(true);

        } else {
          const payload: IBooking = {
            fromAddress: this.bookingForm.get('fromAddress')?.value ?? '',
            toAddress: this.bookingForm.get('toAddress')?.value ?? '',
            goodType: this.bookingForm.get('goodType')?.value ?? '',
            weight: +this.bookingForm.get('weight')?.value ?? 0,
            pricing: +this.bookingForm.get('pricing')?.value ?? 0,
            bookedTime: this.bookingForm.get('bookedTime')?.value ?? '',
            assignedDriver: this.bookingForm.get('assignedDriver')?.value?.toString() ?? '',
            isActive: true,
            createdBy: '0',
          }
          const savedResult = await lastValueFrom(this.bookingService.saveBookingAsync(payload).pipe(
            takeUntil(this.destroy$),
            catchError(e => {
              return of(e);
            }),
            finalize(() => {
              this.onAssignment();
              const timeoutRef = setTimeout(() => {
                this.ngBlockUi.stop();
                clearTimeout(timeoutRef);
              }, 2000);
            })
          ));

          // TODO: SHOW TOAST.
          this.onDialogClose();
          this.bookingService.refreshData.emit(true);
        }
      } catch (error) {
        console.log(error);
      }
    }
  }

  onAssignment = (): void => {
    const driverId: number = +this.bookingForm.get('assignedDriver')?.value;
    this.driverService.assignDriver(driverId);
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
