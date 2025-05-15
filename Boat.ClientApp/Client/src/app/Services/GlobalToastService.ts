import { Injectable } from "@angular/core";

export interface ToastInfo {
    header? : string;
    body: string;
    delay?: number;
    type: ToastType
}

export enum ToastType {
    Info,
    Success,
    Warning,
    Error
}

@Injectable({providedIn: 'root'})
export class GlobalToastService {
    toasts : ToastInfo[] = [];

    showSuccessMessage(header: string | undefined, body: string, delay: number | undefined) {
        this.toasts.push({header: header, body: body, delay: delay, type: ToastType.Success});
    };

    showInfoMessage(header: string | undefined, body: string, delay: number | undefined) {
        this.toasts.push({header: header, body: body, delay: delay, type: ToastType.Info});
    };

    showWarningMessage(header: string | undefined, body: string, delay: number | undefined) {
        this.toasts.push({header: header, body: body, delay: delay, type: ToastType.Warning});
    };

    showDangerMessage(header: string | undefined, body: string, delay: number | undefined) {
        this.toasts.push({header: header, body: body, delay: delay,type: ToastType.Error});
    };

    remove(toast : ToastInfo){
        this.toasts = this.toasts.filter(t => t != toast);
    }

    cleas() {
        this.toasts.splice(0, this.toasts.length);
    }
}