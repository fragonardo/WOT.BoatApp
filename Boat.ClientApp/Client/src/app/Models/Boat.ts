export interface Boat {
  id: string
  serialNumber: string
  type: number
  launchingDate: Date
  owner: string
  name: string
}

export interface BoatType {
  id: number,
  name: string
}