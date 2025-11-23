import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../model/customer.model';
import { FormsModule } from '@angular/forms';
import { Navbar } from '../layout/navbar/navbar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  imports: [FormsModule,Navbar,CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
normalizeCustomer(data: any) {
  return {
    id: data.id ?? data.Id,
    fullName: data.fullName ?? data.FullName,
    email: data.email ?? data.Email,
    phone: data.phone ?? data.Phone,
    address: data.address ?? data.Address
  };
}

   customer: any = {};
  isEditing = false;
  backupCustomer: any = {};

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('customer');
    const data = stored ? JSON.parse(stored) : {};
    this.customer = this.normalizeCustomer(data);
    if (!this.customer.email) {
      alert("Invalid session. Please login again.");
      this.router.navigate(['/login']);
    }
  }

  // Enable edit mode
  enableEdit() {
    this.isEditing = true;
    this.backupCustomer = JSON.parse(JSON.stringify(this.customer)); // deep copy
  }

  // Cancel editing
  cancelEdit() {
    this.customer = JSON.parse(JSON.stringify(this.backupCustomer));
    this.isEditing = false;
  }

  // Save profile changes
  saveChanges() {

  const dto = {
    id: this.customer.id,
    fullName: this.customer.fullName,
    phone: this.customer.phone,
    address: this.customer.address,
    email: this.customer.email
  };

  this.http.put('https://localhost:7110/api/Auth/update', dto)
    .subscribe({
      next: (updated: any) => {

        // Normalize and refresh UI
        const refreshed = this.normalizeCustomer(updated);
        this.customer = { ...refreshed };   // important UI refresh trick

        // update local storage
        localStorage.setItem('customer', JSON.stringify(this.customer));

        // exit edit mode
        this.isEditing = false;

        // refresh backup so further edits work
        this.backupCustomer = { ...this.customer };

        alert("Profile updated successfully!");
        this.router.navigate(['/dashboard']);

      },
      error: () => alert("Update failed!")
    });
}






}
