import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';

@Component({
  selector: 'app-pay-contact',
  imports: [CommonModule, Navbar],
  templateUrl: './pay-contact.html',
  styleUrl: './pay-contact.css',
})
export class PayContact implements OnInit {

  contacts: any[] = [];
  loading = true;
 customerId!: number;
  constructor(
    private http: HttpClient,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const user = JSON.parse(localStorage.getItem('customer') || '{}');
    this.customerId = user.id;
    this.loadContacts();
  }

  loadContacts() {
    this.http.get(`https://localhost:7110/api/Contact/paynet/${this.customerId}`)
      .subscribe({
        next: (res: any) => {
          console.log("CONTACT RESPONSE →", res);

          this.contacts = res;
          this.loading = false;

          this.cd.detectChanges();  // ⭐ Force UI update
        },
        error: err => {
          console.error(err);
          this.loading = false;
          this.cd.detectChanges();
        }
      });
  }

  selectContact(c: any) {
  this.router.navigate(['/send-money'], {
    state: {
      receiverCustomerId: c.id,
      phone: c.phone,
      name: c.name
    }
  });
}


}
