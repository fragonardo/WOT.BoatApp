import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./core/header/header.component";
import { FooterComponent } from "./shared/footer/footer.component";
import { GlobalToastContainerComponent } from './core/global-toast-container/global-toast-container.component';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent,  GlobalToastContainerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';
}
