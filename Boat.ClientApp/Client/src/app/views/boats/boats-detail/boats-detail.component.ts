import { Component, inject, Input, OnInit, signal } from '@angular/core';
import { Boat, BoatType } from '../../../Models/Boat';
import { BoatHttpService } from '../../../Services/BoatHttp.Service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { catchError, finalize, identity, map, mergeMap, switchMap, tap } from 'rxjs';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { format } from 'date-fns';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-boats-detail',
  imports: [ReactiveFormsModule, ReactiveFormsModule],
  templateUrl: './boats-detail.component.html',
  styleUrl: './boats-detail.component.css'
})
export class BoatsDetailComponent implements OnInit {

  private boat: Boat = <Boat>{};
  private activatedRoute = inject(ActivatedRoute);
  private mode: ModeEdition = ModeEdition.creation;  
  public isLoading = signal<boolean>(false); 
  public boatTypes : BoatType[] = [];
  private router = inject(Router);
  public formBoat: FormGroup = new FormGroup({
        identity: new FormControl(),
        serialNumber: new FormControl(),
        owner : new FormControl(),
        name : new FormControl(),
        launchingDate : new FormControl(),
        type : new FormControl(),
      }); 
      activeModal = inject(NgbActiveModal);

  constructor(private service : BoatHttpService) {}
  @Input() Id: string|null = null;

  ngOnInit(): void {
    
    //const id = this.activatedRoute.snapshot.paramMap.get('id');    
    if(this.Id !== null)
    {
      this.mode = ModeEdition.modification;
      this.service.getBoatById$(this.Id)
        .subscribe((next) => {
          this.boat = next;   
          this.initForm();
        } );      
    }
    
    this.service.getBoatType$()
      .subscribe((result : BoatType[]) => {
        this.boatTypes = result;
      })
  }

  public Save(){        
    this.boat.serialNumber = this.formBoat.get('serialNumber')?.value;
    this.boat.type = this.formBoat.get('type')?.value;
    this.boat.launchingDate = this.formBoat.get('launchingDate')?.value;
    this.boat.owner = this.formBoat.get('owner')?.value;
    this.boat.name = this.formBoat.get('name')?.value;

    this.isLoading.set(true);
    let operation$ = this.mode === ModeEdition.creation ? this.service.createBoat$(this.boat) : this.service.updateBoat$(this.boat)

    operation$.pipe(
        tap(result => {
          //Afficher le succès 
        }),        
        catchError(err => err),// afficher l'erreur
        finalize(()=>{ this.isLoading.set(false);  })     
      ).subscribe()    
  }

  public cancel()
  {
    this.router.navigate([`boats/`])
  }

  public delete()
  {
    this.isLoading.set(true);
    this.service
    .deleteBoat$(this.boat.id).pipe(
        tap(result => {
          // Afficher le succès 
        }),        
        //catchError(err => err), afficher l'erreur
        finalize(()=>{ 
          this.isLoading.set(false);
          this.router.navigate([`boats/`]);
        })     
      ).subscribe()   
  }
  
  private initForm()
  {
    this.formBoat = new FormGroup({
        'identity': new FormControl(this.boat.id),
        serialNumber: new FormControl(this.boat.serialNumber),
        owner : new FormControl(this.boat.owner),
        name : new FormControl(this.boat.name),
        launchingDate : new FormControl(format(this.boat.launchingDate, 'yyyy-MM-dd')),
        type : new FormControl(this.boat.type),
      })
  }
}

export enum ModeEdition 
{
  creation,
  modification
}

export enum MessageType
{
  Info,
  Warning,
  Error
}