import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Auth } from '../Services/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  customerName = 'User';
  customerData: any = null;

  sections = [
    {
      title: 'Transfers',
      items: [
        { name: 'Pay Contact', route: '/pay-contact', icon: '👤' },
        { name: 'Pay to Bank', route: '/pay-bank', icon: '🏦' },
        { name: 'Scan QR', route: '/scan-qr', icon: '🔳' },
        { name: 'Self Transfer', route: '/self-transfer', icon: '🔁' }
      ]
    },
    {
      title: 'Bills & Recharge',
      items: [
        { name: 'Mobile Recharge', route: '/recharge/mobile', icon: '📱' },
        { name: 'Electricity', route: '/bills/electricity', icon: '💡' },
        { name: 'DTH', route: '/recharge/dth', icon: '📺' },
        { name: 'FASTag', route: '/fastag', icon: '🚗' }
      ]
    },
    {
      title: 'Services',
      items: [
        { name: 'Loans & EMI', route: '/loans', icon: '🏦' },
        { name: 'Insurance', route: '/insurance', icon: '🛡️' },
        { name: 'Subscriptions', route: '/subscriptions', icon: '🔔' }
      ]
    },
    {
      title: 'Accounts',
      items: [
        { name: 'Linked Banks', route: '/accounts', icon: '🧾' },
        { name: 'Cards', route: '/cards', icon: '💳' },
        { name: 'UPI IDs', route: '/upi', icon: '🔗' }
      ]
    }
  ];

  constructor(private router: Router, private auth: Auth) {}

  ngOnInit(): void {
    try {
      const c = localStorage.getItem('customer');
      this.customerData = c ? JSON.parse(c) : null;
      this.customerName = this.customerData?.FullName || this.customerData?.fullName || 'PayNet User';
    } catch {
      this.customerName = 'PayNet User';
    }
  }

  navigate(path: string) {
    if (!path) return;
    this.router.navigate([path]);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
