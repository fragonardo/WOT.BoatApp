import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule }  from '@angular/material/button';
import { MatIconModule }  from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-header',
  imports: [RouterModule, MatButtonModule, MatIconModule, MatToolbarModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}
