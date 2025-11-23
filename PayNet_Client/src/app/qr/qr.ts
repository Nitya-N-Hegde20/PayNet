import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { QRService } from '../Services/qrservice';
import { Navbar } from '../layout/navbar/navbar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-qr',
  imports: [Navbar,CommonModule],
  templateUrl: './qr.html',
  styleUrl: './qr.css',
})
export class QR implements OnInit {

  qrUrl: string = "";
  customerId: number = 0;
  fullName: string = "";

  constructor(private qr: QRService,private cd: ChangeDetectorRef) {}

  ngOnInit() {
    const stored = JSON.parse(localStorage.getItem("customer")!);
    this.customerId = stored.id;
    this.fullName = stored.fullName;

    this.qr.getQRCode(this.customerId).subscribe((res: any) => {
    
      this.qrUrl = res.qrUrl;
       this.cd.detectChanges();
    });
  
  }
}
