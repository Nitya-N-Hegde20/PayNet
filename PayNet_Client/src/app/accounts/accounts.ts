import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';

@Component({
  selector: 'app-accounts',
  imports: [CommonModule,Navbar],
  templateUrl: './accounts.html',
  styleUrl: './accounts.css',
})
export class Accounts implements OnInit {

  accounts: any[] = [];
  customerId!: number;

  constructor(private http: HttpClient, private router: Router,private cd:ChangeDetectorRef) {}

  ngOnInit(): void {
  const customer = JSON.parse(localStorage.getItem('customer') || '{}');
  this.customerId = customer.id;

  this.http
    .get<any[]>(`https://localhost:7110/api/Account/customer/${this.customerId}`)
    .subscribe(res => {
      console.log('ACCOUNTS →', res);
      this.accounts = res;
      this.cd.detectChanges(); // ⭐ CRITICAL
    });
}

  viewDetails(acc: any) {
    this.router.navigate(['/account-details', acc.accountNumber]);
  }
}
