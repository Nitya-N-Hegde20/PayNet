import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'console';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  const authReq = token ? req.clone({
    setHeaders:{
      Authorization :`Bearer ${token}`,
    },
  }):req;
  return next(authReq).pipe(
    catchError((error :HttpErrorResponse) =>
    {
      if (error.status === 401)
      {
        alert('Session expired Please Log In');
        localStorage.clear();
        router.navigate(['/login']);
      }
      return throwError(() =>error);
    })
  );
};
