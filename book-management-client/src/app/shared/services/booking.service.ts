import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'

import { environment } from '../../../environments/environment';
import { IBooking } from '../interfaces/booking.interface';
import { IPaginationData } from '../interfaces/pagination-data.interface';

@Injectable()
export class BookingService {
  private baseUrl: string = environment.serverUrl;

  public refreshData = new EventEmitter<boolean>();

  constructor(private httpClient: HttpClient) { }

  getAllBookingDetails = (page: number, pageSize: number): Observable<IPaginationData<IBooking>> => {
    const url: string = `${this.baseUrl}/api/v1/booking/query?page=${page}&pageSize=${pageSize}`
    return this.httpClient.get<IPaginationData<IBooking>>(url);
  }

  saveBookingAsync = (payload: IBooking): Observable<IBooking> => {
    const url: string = `${this.baseUrl}/api/v1/booking`;
    return this.httpClient.post<IBooking>(url, payload);
  }

  updateBookingAsync = (payload: IBooking): Observable<IBooking> => {
    const url: string = `${this.baseUrl}/api/v1/booking`;
    return this.httpClient.put<IBooking>(url, payload);
  }

  deleteBookingAsync = (bookingId: string | undefined): Observable<boolean> => {
    const url: string = `${this.baseUrl}/api/v1/booking/${bookingId}`;
    return this.httpClient.delete<boolean>(url);
  }
}
