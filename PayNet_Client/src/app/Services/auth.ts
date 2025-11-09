import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
const API_BASE = 'https://localhost:7110/api/Auth';
@Injectable({
  providedIn: 'root',
})
export class Auth {
  constructor(private http: HttpClient) {}

  // send Google ID token to backend, backend validates and returns either requiresPhone or token
  googleLogin(idToken: string): Observable<any> {
    return this.http.post<any>(`${API_BASE}/google-login`, { idToken });
  }

  // finalize registration for Google user by adding phone and creating user
completeGoogleRegistration(payload: any) {
  return this.http.post<any>(`${API_BASE}/complete-google-registration`, payload);
}

  // optional: normal login
  login(dto: { email: string; password: string }) {
    return this.http.post<any>(`${API_BASE}/login`, dto);
  }
  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  // 🔹 Get saved token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // 🔹 Clear login info (logout)
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('customer');
  }
}
