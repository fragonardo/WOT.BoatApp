import { Boat, BoatType } from "../Models/Boat";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from "../../environments/environment.development";
import { Observable } from "rxjs";
import { FilterBoatRequest } from "../Requests/FilterBoatRequest";
import { apiCollectionResult } from "../Models/apiCollectionResult";

@Injectable({
    providedIn: 'root'
})
export class BoatHttpService {
    private apiUrl : string = `${environment.apiUrl}/boats`; ;
    
    httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

    constructor(private http : HttpClient){}
    
    getBoats$(parameter : QueryParameter) : Observable<apiCollectionResult<Boat>>{        
        return this.http.get<apiCollectionResult<Boat>>(`${this.apiUrl}?PageIndex=${parameter.pageIndex}&ItemPerPage=${parameter.itemPerPage}&Filter=${parameter.filter}`, this.httpOptions);
    }

    
    createBoat$(boat : Boat) : Observable<string>{  
        console.log('apiUrl:', this.apiUrl);
        console.log('boat:', boat);
        return this.http.post<string>(this.apiUrl, boat, this.httpOptions);
    }

    getBoatById$(id: string) : Observable<Boat>{
        console.log(`${this.apiUrl}/${id}`);
        return this.http.get<Boat>(`${this.apiUrl}/${id}`);
    }

    updateBoat$(boat: Boat) : Observable<string>{        
        return this.http.put<string>(`${this.apiUrl}/${boat.id}`, boat, this.httpOptions);
    }

    deleteBoat$(id: string): Observable<boolean> {
        return this.http.delete<boolean>(`${this.apiUrl}/${id}`, this.httpOptions);
    }

    getBoatType$(): Observable<BoatType[]> {
        return this.http.get<BoatType[]>(`${this.apiUrl}/types`, this.httpOptions);
    }
}

export interface QueryParameter{
    pageIndex : number,
    itemPerPage: number,
    filter : string
}