import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { RouterModule } from '@angular/router';

import { ApplicationHeaderComponent } from './components/application-header/application-header.component';
import { ApplicationNavigationComponent } from './components/application-navigation/application-navigation.component';


@NgModule({
  declarations: [
    ApplicationHeaderComponent,
    ApplicationNavigationComponent
  ],
  exports: [
    ApplicationHeaderComponent,
    ApplicationNavigationComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    RouterModule
  ]
})
export class SharedModule { }
