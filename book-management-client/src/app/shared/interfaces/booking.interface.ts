export interface IBooking {
  id?: number
  uniqueId?: string
  fromAddress: string
  toAddress: string
  goodType: string
  weight: number
  pricing: number
  bookedTime: string
  assignedDriver: string,
  isActive: boolean
  createdBy: string
  createdDateTime?: string
  updatedBy?: string
  updatedDateTime?: string
}
