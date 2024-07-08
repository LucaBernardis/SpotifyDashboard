import { DashboardData } from '../../models/dashboardData';
import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from "../header/header.component";
import { MultiListComponent } from "../multi-list/multi-list.component";
import { DashboardApiService } from '../../services/api/dashboard-api.service';
import { Config } from '../../models/config';
import { CardComponent } from "../card/card.component";
import { ListComponent } from '../list/list.component';
import { PositionPipe } from '../../pipes/position.pipe';

@Component({
    selector: 'app-dashboard-wrapper',
    standalone: true,
    templateUrl: './dashboard-wrapper.component.html',
    styleUrls: ['./dashboard-wrapper.component.css'],
    
    imports: [HeaderComponent, ListComponent, CardComponent, MultiListComponent, PositionPipe],
})
export class DashboardWrapperComponent implements OnInit{


   // The object that contains the result of the dashboard api call
    public dashboardData: DashboardData | null;
    public dashboardConfig: Config[] = [];

    constructor(private dashboardApiService: DashboardApiService){
        this.dashboardData = null;
    }
    
    // Wrapper component that contains all the dashboard components
    // Gather together all the data from the backend methods and pass them with the @Input() Decorator to the respective components
    // with this single call u can remove all the
    // other services cause all the backend calls are handled by this service
    ngOnInit(): void {
        this.dashboardApiService.getDashboardData().subscribe((data) => { 
          this.dashboardData = data;
            console.log(this.dashboardData);
        })

        // Next Step: Call the DashboardConfig Service to manage the widgets to display
        this.dashboardApiService.getDashboardConfig().subscribe((config) => {
            this.dashboardConfig = config;
            console.log(this.dashboardConfig);
        })
       
    }
}
