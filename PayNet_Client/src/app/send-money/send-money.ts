import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';

@Component({
  selector: 'app-send-money',
  imports: [CommonModule,FormsModule,Navbar],
  templateUrl: './send-money.html',
  styleUrl: './send-money.css',
})
export class SendMoney implements OnInit {

  // 🔹 properties used in HTML MUST exist here
  accounts: any[] = [];

  fromAccountId!: number;
  toAccountId!: number;
  amount!: number;

  customerId!: number;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const customer = JSON.parse(localStorage.getItem('customer') || '{}');
    this.customerId = customer.id;

    this.loadAccounts();
  }

  loadAccounts() {
    this.http
      .get<any[]>(`https://localhost:7110/api/Account/customer/${this.customerId}`)
      .subscribe({
        next: (res) => this.accounts = res,
        error: () => alert('Failed to load accounts')
      });
  }

  send() {
    const payload = {
      fromAccountId: this.fromAccountId,
      toAccountId: this.toAccountId,
      amount: this.amount
    };

    this.http
      .post('https://localhost:7110/api/Transaction/send', payload)
      .subscribe({
        next: () => alert('Money sent successfully'),
        error: () => alert('Transaction failed')
      });
  }
}