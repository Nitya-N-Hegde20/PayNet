import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../Services/account';
import { Router } from '@angular/router';
import { CreateAccountDTO } from '../model/createaccountDTO.model';
import { CommonModule } from '@angular/common';
import { Navbar } from '../layout/navbar/navbar';

@Component({
  selector: 'app-create-account',
  imports: [FormsModule,CommonModule,Navbar],
  templateUrl: './create-account.html',
  styleUrl: './create-account.css',
})
export class CreateAccount {
banks: any[] = [];
  branches: any[] = [];

  form: CreateAccountDTO = new CreateAccountDTO();

  constructor(private service: Account, private router: Router) {}

 ngOnInit(): void {
  this.service.getBankList().subscribe({
    next: (data) => this.banks = data,
    error: (err) => console.error('Bank loading failed:', err),
  });

  const user = JSON.parse(localStorage.getItem('customer') || '{}');
  this.form.customerId = user.id;
}

  onBankSelect() {
    const bank = this.banks.find((b) => b.code === this.form.bankCode);
    this.form.bankName = bank.name;
    this.branches = bank.branches;
  }

  onBranchSelect() {
    const branch = this.branches.find((b: any) => b.name === this.form.branchName);
    this.form.ifsc = branch.ifsc;
  }

  createAccount() {
    this.service.createAccount(this.form).subscribe({
      next: (res) => {
        alert('Account Created: ' + res.accountNumber);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        alert('Failed to create account');
        console.error(err);
      },
    });
  }
}
