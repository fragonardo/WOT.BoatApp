export class FilterBoatRequest {
  SerialNumberFilter?: string;
  //type: number
  //launchingDate?: Date
  OwnerFilter?: string;
  NameFilter?: string;
  PageIndex?:number;
  ItemPerPage?: number;
}