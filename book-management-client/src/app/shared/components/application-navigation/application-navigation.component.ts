import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { INavigationIem } from '../../interfaces/navigation.interface';

@Component({
  selector: 'app-application-navigation',
  templateUrl: './application-navigation.component.html',
  styleUrls: ['./application-navigation.component.scss']
})
export class ApplicationNavigationComponent {
  menu: INavigationIem[] = [
    {
      displayName: 'Booking Management',
      iconName: 'desktop_windows',
      route: '/dashboard/booking/details',
    },
  ];

  constructor(private router: Router) { }

  onRouteClick = (route: string | undefined): void => {
    if (route) {
      this.router.navigate([route]);
    }
  }
}
