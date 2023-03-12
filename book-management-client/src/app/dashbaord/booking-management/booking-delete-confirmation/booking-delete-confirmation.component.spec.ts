import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingDeleteConfirmationComponent } from './booking-delete-confirmation.component';

describe('BookingDeleteConfirmationComponent', () => {
  let component: BookingDeleteConfirmationComponent;
  let fixture: ComponentFixture<BookingDeleteConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookingDeleteConfirmationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookingDeleteConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
