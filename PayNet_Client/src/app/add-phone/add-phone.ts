import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../Services/auth';

@Component({
  selector: 'app-add-phone',
  imports: [FormsModule],
  templateUrl: './add-phone.html',
  styleUrl: './add-phone.css',
})
export class AddPhone {
 phone = '';
  googleUser: any;

  constructor(private auth: Auth, private router: Router) {
    this.googleUser = this.router.getCurrentNavigation()?.extras?.state?.['googleUser'] || {};
  }

  submitPhone() {
    if (!this.phone) { alert('Enter phone number'); return; }

    const payload = {
      email: this.googleUser.Email || this.googleUser.email,
      fullName: this.googleUser.FullName || this.googleUser.name,
      phone: this.phone
    };

    this.auth.completeGoogleRegistration(payload).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.token);
        alert('Registration Successful');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error(err);
        alert('Failed to complete registration');
      }
    });
  }
}
