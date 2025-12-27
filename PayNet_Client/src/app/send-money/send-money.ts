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
accounts: any[] = [];

  fromAccountNumber!: string;   // ⭐ STRING
  receiverAccountNumber!: string;

  receiverCustomerId!: number;
  receiverPhone!: string;
  receiverName!: string;

  amount!: number;
  customerId!: number;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const customer = JSON.parse(localStorage.getItem('customer') || '{}');
    this.customerId = customer.id;

    const nav = history.state;

    if (nav?.receiverCustomerId) {
      this.receiverCustomerId = nav.receiverCustomerId;
      this.receiverPhone = nav.phone;
      this.receiverName = nav.name;

      this.loadReceiverAccount(nav.receiverCustomerId);
    }

    this.loadMyAccounts();
  }

  // ⭐ Load user's accounts
  loadMyAccounts() {
    this.http
      .get<any[]>(`https://localhost:7110/api/Account/customer/${this.customerId}`)
      .subscribe(res => {
        this.accounts = res; // They already have accountNumber
      });
  }

  // ⭐ Load receiver first account
  loadReceiverAccount(custId: number) {
    this.http
      .get<any[]>(`https://localhost:7110/api/Account/customer/${custId}`)
      .subscribe(res => {
        if (res.length > 0) {
          this.receiverAccountNumber = res[0].accountNumber;
        }
      });
  }

  send() {
    const payload = {
      fromAccountId: this.fromAccountNumber,   // ⭐ STRING accountNumber
      toAccountId: this.receiverAccountNumber, // ⭐ STRING accountNumber
      amount: this.amount
    };

    this.http
      .post('https://localhost:7110/api/Transaction/send', payload)
      .subscribe({
        next: () => {
          alert('Money sent successfully');
          this.router.navigate(['/dashboard']);
        },
        error: () => alert('Transaction failed')
      });
  }
}