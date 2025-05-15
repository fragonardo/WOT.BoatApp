import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, TemplateRef } from '@angular/core';
import { Boat, BoatType } from '../../../Models/Boat';
import { environment } from '../../../../environments/environment.development';
import { CommonModule, DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { BoatHttpService } from '../../../Services/BoatHttp.Service';
import { FilterBoatRequest } from '../../../Requests/FilterBoatRequest';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, map } from 'rxjs';
import {NgbActiveModal, NgbConfig} from '@ng-bootstrap/ng-bootstrap';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { GlobalToastService } from '../../../Services/GlobalToastService';

@Component({
  selector: 'app-boats-list',
  imports: [CommonModule, MatTableModule],
  templateUrl: './boats-list.component.html',
  styleUrl: './boats-list.component.css',
  providers: [NgbModalConfig, NgbModal]
})
export class BoatsListComponent implements OnInit {
  
   public boats: Boat[] = [];
   public isLoading = signal<boolean>(false);
   private router = inject(Router);
   private toastService = inject(GlobalToastService);
   public boatTypeMap : Map<number, string> = new Map();
   
   constructor(
    config: NgbModalConfig,
    private modalService : NgbModal,
    private service : BoatHttpService) {
      config.backdrop = 'static';
      config.keyboard = false;    
  }

   ngOnInit() {
      this.isLoading.set(true);

      this.service.getBoatType$().pipe(
        map((result : BoatType[]) => 
          result.map((x) => this.boatTypeMap.set(x.id, x.name)))          
        ).subscribe()     

      this.service.getBoats$()
          .subscribe({
              next: (result : Boat[]) => { this.boats = result; this.isLoading.set(false) },
              error: (error: any) => console.error(error)
          });
  }

  public onClick(boat : Boat){    
    this.router.navigate([`boats/${boat.id}`])
  }

  public AddNew(){
    this.router.navigate([`boats/new`])
  }

  public delete(id: string, content:TemplateRef<any>){
    this.modalService
    .open(content)
    .result.then(
      null,
      (reason)=> {
        //this.service.deleteBoat$(id)
        this.toastService.showDangerMessage(undefined, 'Salut, je suis un toast', 5000 );
      }
    );
    
  }
}
