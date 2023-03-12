import { Injectable } from '@angular/core';

// TODO : BELOW DRIVERS SHOULD BE LOADED FROM THE BACKEND.
const driverData = [
  {
    id: 1,
    driverName: 'Shantha-1',
    isAssigned: false,
  },
  {
    id: 2,
    driverName: 'Shantha-2',
    isAssigned: false,
  },
  {
    id: 3,
    driverName: 'Shantha-3',
    isAssigned: false,
  },
  {
    id: 4,
    driverName: 'Shantha-4',
    isAssigned: false,
  },
  {
    id: 5,
    driverName: 'Shantha-5',
    isAssigned: false,
  }
]

@Injectable({
  providedIn: 'root'
})
export class DriverService {

  constructor() { }

  getDriverData = (): any => {
    return driverData.filter(x => !x.isAssigned);
  }

  assignDriver = (id: number): void => {
    const index = driverData.findIndex(x => x.id === id);
    driverData[index].isAssigned = !driverData[index].isAssigned;
  }
}
