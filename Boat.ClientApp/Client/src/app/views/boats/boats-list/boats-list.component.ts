import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, TemplateRef } from '@angular/core';
import { Boat, BoatType } from '../../../Models/Boat';
import { CommonModule, DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { BoatHttpService } from '../../../Services/BoatHttp.Service';
import {  Router } from '@angular/router';
import { delay, map } from 'rxjs';
import {NgbActiveModal, NgbConfig, NgbPaginationModule} from '@ng-bootstrap/ng-bootstrap';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { GlobalToastService } from '../../../Services/GlobalToastService';
import { BoatsDetailComponent } from '../boats-detail/boats-detail.component';
import { apiCollectionResult } from '../../../Models/apiCollectionResult';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-boats-list',
  imports: [CommonModule, MatTableModule, NgbPaginationModule, ReactiveFormsModule ],
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
   public page : number = 1;
   public pageSize : number = 10;
   public totalItems : number = 0;
   public filter = new FormControl('');

   
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
        ).subscribe() ;
        
        this.loadBoats();
  }

  public onClick(boat : Boat){    
    const modalRef = this.modalService.open(BoatsDetailComponent);
    modalRef.componentInstance.Id = boat.id;
  }

  public AddNew(){
    const modalRef = this.modalService.open(BoatsDetailComponent);    
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

  public onPageChange(){
    this.loadBoats();
  }

  public Search(){
    this.loadBoats();
  }

  private loadBoats(){
    this.service.getBoats$({ pageIndex: this.page, itemPerPage: this.pageSize, filter : this.filter.value ?? ''})
          .subscribe({
              next: (result : apiCollectionResult<Boat>) => { 
                this.boats = result.data; 
                this.totalItems = result.totalCount;
                this.isLoading.set(false) },
              error: (error: any) => console.error(error)
          });
  }
}
