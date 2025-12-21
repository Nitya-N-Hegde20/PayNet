import { AfterViewInit, Component, NgZone, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Console } from 'console';
import { HttpClient } from '@angular/common/http';
import { LoginDTO } from '../model/loginDTO.model';
import { Router } from '@angular/router';
import { Auth } from '../Services/auth';
import { Navbar } from '../layout/navbar/navbar';
import { environment } from '../../Environment/environment';
declare const google: any;
@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login implements OnInit {
  login: LoginDTO = new LoginDTO();
  loading = false;

  constructor(private http: HttpClient, private router: Router, private auth: Auth) {}

  ngOnInit() {
    // ✅ Initialize Google Sign-In
    google.accounts.id.initialize({
      client_id: environment.googleClientId,
      callback: (response: any) => this.handleGoogleResponse(response),
    });

    // ✅ Render the Google Sign-In button
    google.accounts.id.renderButton(document.getElementById('googleBtn'), {
      theme: 'outline',
      size: 'large',
      text: 'signin_with',
      shape: 'rectangular',
      width: 280,
    });
  }

 handleGoogleResponse(response: any) {
  const idToken = response.credential;

  this.auth.googleLogin(idToken).subscribe({
    next: (res) => {
      if (res.requiresPhone) {
        // Navigate to add-phone with data
        this.router.navigate(['/addphone'], { state: { googleUser: res } });
      } else {
        this.auth.saveToken(res.token);
        localStorage.setItem('customer', JSON.stringify(res.customer));
        alert('Login Successful via Google');
        this.router.navigate(['/dashboard']);
      }
    },
    error: (err) => {
      console.error('Google login failed', err);
      alert('Google login failed. Please try again.');
    },
  });
}

navigateTo(path: string) {
    this.router.navigate([path]);
  }
  onLogin() {
    this.loading = true;
    this.auth.login(this.login).subscribe({
      next: (response) => {
        this.auth.saveToken(response.token);
        localStorage.setItem('customer', JSON.stringify(response.customer));
        alert('Login Successful');
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        this.loading = false;
        alert('Invalid email or password');
      },
    });
  }
}