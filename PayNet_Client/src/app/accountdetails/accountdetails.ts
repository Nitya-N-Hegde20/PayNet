import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-accountdetails',
  imports: [Navbar,CommonModule],
  templateUrl: './accountdetails.html',
  styleUrl: './accountdetails.css',
})
export class Accountdetails implements OnInit {

  account: any;
  loading = true;

  constructor(
    private http: HttpClient,
    private cd: ChangeDetectorRef,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const nav = history.state;
   const accountNumber = this.route.snapshot.params['accNumber'];

    if (!accountNumber) {
      alert('Invalid navigation');
      return;
    }

    this.http
      .get(`https://localhost:7110/api/Account/details/${accountNumber}`)
      .subscribe(res => {
        this.account = res;
        this.loading = false;
        this.cd.detectChanges(); // ⭐ hydration fix
      });
  }
}
