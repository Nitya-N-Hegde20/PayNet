import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-self-transfer',
  imports: [CommonModule,Navbar,FormsModule],
  templateUrl: './self-transfer.html',
  styleUrl: './self-transfer.css',
})
export class SelfTransfer implements OnInit {

  accounts: any[] = [];

  fromAccountNumber!: string;
  toAccountNumber!: string;
  amount!: number;

  customerId!: number;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const customer = JSON.parse(localStorage.getItem('customer') || '{}');
    this.customerId = customer.id;

    this.loadAccounts();
  }

  loadAccounts() {
    this.http
      .get<any[]>(`https://localhost:7110/api/Account/customer/${this.customerId}`)
      .subscribe({
        next: res => this.accounts = res,
        error: () => alert('Failed to load accounts')
      });
  }

  transfer() {
    if (this.fromAccountNumber === this.toAccountNumber) {
      alert('From and To accounts cannot be the same');
      return;
    }

    const payload = {
      fromAccountId: this.fromAccountNumber,
      toAccountId: this.toAccountNumber,
      amount: this.amount
    };

    this.http
      .post('https://localhost:7110/api/Transaction/send', payload)
      .subscribe({
        next: () => {
          alert('Self transfer successful');
          this.router.navigate(['/dashboard']);
        },
        error: () => alert('Transfer failed')
      });
  }

}
