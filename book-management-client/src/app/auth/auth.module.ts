import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';

import { AuthLoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth.routing';
import { metaReducers, userReducer } from '../redux/reducers/user-reducer';

@NgModule({
  declarations: [
    AuthLoginComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    StoreModule.forFeature('user', userReducer, { metaReducers })
  ],
})
export class AuthModule { }
