import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { FormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

import { IAuth } from '../../shared/interfaces/auth.interface';
import { AuthService } from '../../shared/services/auth.service';
import { AddUserToken } from 'src/app/redux/actions/user-action';
import { UserTokenModel } from 'src/app/shared/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class AuthLoginComponent implements OnInit, OnDestroy {


  @BlockUI() ngBlockUi!: NgBlockUI;

  loginForm!: FormGroup;
  destroy$ = new Subject<boolean>();

  constructor(private authService: AuthService, private store: Store, private router: Router) { }

  ngOnInit(): void {
    this.initializeFormGroup();
  }

  initializeFormGroup = (): void => {
    this.loginForm = new FormGroup({
      userName: new UntypedFormControl('', [Validators.required]),
      password: new UntypedFormControl('', [Validators.required])
    });
  }

  onAuthLogin = (): void => {
    this.ngBlockUi.start('Authenticating')
    Object.keys(this.loginForm.controls).forEach(field => {
      const control = this.loginForm.get(field);
      if (control) {
        control.markAsTouched({ onlySelf: true });
      }
    });

    if (this.loginForm.valid) {

      const payload: IAuth = {
        userName: this.loginForm.get('userName')?.value,
        password: this.loginForm.get('password')?.value
      };

      this.authService.onAuthLogin(payload).pipe(takeUntil(this.destroy$)).subscribe({
        next: (response: UserTokenModel) => {
          this.store.dispatch(new AddUserToken(response));
          this.authService.processAuthConfiguration(response)
          this.router.navigate(['/dashboard']);
          this.ngBlockUi.stop();
        },
        error: (() => {
          this.ngBlockUi.stop();
        })
      })
    }
  }


  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
