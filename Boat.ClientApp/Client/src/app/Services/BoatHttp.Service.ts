import { Boat, BoatType } from "../Models/Boat";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from "../../environments/environment.development";
import { Observable } from "rxjs";
import { FilterBoatRequest } from "../Requests/FilterBoatRequest";

@Injectable({
    providedIn: 'root'
})
export class BoatHttpService {
    private apiUrl : string = `${environment.apiUrl}/boats`; ;
    
    httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

    constructor(private http : HttpClient){}
    
    getBoats$() : Observable<Boat[]>{
        let pageIndex = 2;
        let itemPerPage = 10;
        let filter = 'test';
        return this.http.get<Boat[]>(`${this.apiUrl}?PageIndex=${pageIndex}&ItemPerPage=${itemPerPage}&Filter=${filter}`, this.httpOptions);
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