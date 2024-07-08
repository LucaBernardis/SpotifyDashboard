import { APP_INITIALIZER, ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { httpInterceptor } from './http.interceptot';
import { SpotifyAuthService } from './services/spotify-auth.service';
import { tap } from 'rxjs';
import { DashboardService } from './services/dashboard.service';
import { PositionPipe } from './pipes/position.pipe';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([httpInterceptor]), withFetch()),
    {
      // When the application is initialized it calls the method the check the authentication and eventually redirect to the spotify authentication page
      provide: APP_INITIALIZER,
      useFactory: (spotifyAuthService: SpotifyAuthService, dashboardService: DashboardService) =>
        () => spotifyAuthService.checkAuthentication()
          .pipe(
            tap(token => {
              // Passign the token value between the main services to always have it in the api calls
              dashboardService.setToken(token);
            }),
          ),
      deps: [SpotifyAuthService, DashboardService],
      multi: true
    },
    PositionPipe
  ],
  
};
