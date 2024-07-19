import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DashboardWrapperComponent } from "./components/dashboard-wrapper/dashboard-wrapper.component";
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, DashboardWrapperComponent, CommonModule]
})
export class AppComponent {
  title = 'Spotify Dashboard';
}
