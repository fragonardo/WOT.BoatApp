import { Component, inject } from '@angular/core';
import { GlobalToastService, ToastType } from '../../Services/GlobalToastService';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'global-toast-container',
  imports: [NgbToastModule],
  template: `
    @for (toast of toastService.toasts; track toast){
      <ngb-toast
        [class]="getClassName(toast.type)"
        [autohide]="true"
        [delay]="toast.delay || 5000"
        (hidden)="toastService.remove(toast)"
        >
        {{ toast.body }}
      </ngb-toast>
    }
  `,
  styles: ``,
  host: { class: 'toast-container position-fixed end-0 p-2 fw-bolder', style: 'z-index: 1200' },
})
export class GlobalToastContainerComponent {
  toastService = inject(GlobalToastService);

  getClassName(type : ToastType){
    let className = '';

    switch (type){
      
      case ToastType.Success:
        className = 'bg-success text-light';
        break;

      case ToastType.Warning:
        className = 'bg-warning text-light';
        break;
      
      case ToastType.Error:
        className = 'bg-danger text-light';
        break;

      default:
        className = 'bg-primary text-light';
    }
    return className;
  }
}
