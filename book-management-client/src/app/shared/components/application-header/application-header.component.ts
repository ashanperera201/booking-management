import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { MatDrawer } from '@angular/material/sidenav';

@Component({
  selector: 'app-application-header',
  templateUrl: './application-header.component.html',
  styleUrls: ['./application-header.component.scss']
})
export class ApplicationHeaderComponent {
  @Input() drawer!: MatDrawer;

  constructor(private router: Router) { }

  onLogout = (): void => {
    this.router.navigate(['/auth/login']);
  }
}
