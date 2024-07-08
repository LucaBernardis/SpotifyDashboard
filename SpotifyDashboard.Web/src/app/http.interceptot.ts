import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const httpInterceptor: HttpInterceptorFn = (req, next) => {

  // Check if your http request containts "serverApi"
  if (req.url.includes('serverApi')) {

    // If its true it intercepts the call and add the backend address as prefix
    const authReq = req.clone({
      url: `https://localhost:7199${req.url}`,
    });

    // Pass the cloned request with the updated header to the next handler
    return next(authReq).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          // Handle HTTP errors
          if (err.status === 401) {
            // Specific handling for unauthorized errors
            console.error('Unauthorized request:', err);
          } else {
            // Handle other HTTP error codes
            console.error('HTTP error:', err);
          }
        } else {
          // Handle non-HTTP errors
          console.error('An error occurred:', err);
        }
        // Re-throw the error to propagate it further
        return throwError(() => err);
      })
    );
  } else {
    // If the request doesn't contain "serverApi", just pass it through
    return next(req);
  }
};