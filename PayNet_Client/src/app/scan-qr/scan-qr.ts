import { Component, ElementRef, Inject, OnDestroy, OnInit, PLATFORM_ID, ViewChild } from '@angular/core';
import { Navbar } from '../layout/navbar/navbar';
import { BrowserMultiFormatReader } from '@zxing/library';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule, isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-scan-qr',
  imports: [Navbar,FormsModule,CommonModule],
  templateUrl: './scan-qr.html',
  styleUrl: './scan-qr.css',
})
export class ScanQR implements OnInit, OnDestroy {
@ViewChild('preview', { static: true }) video!: ElementRef<HTMLVideoElement>;

  qrResult: string = '';
  private scanner = new BrowserMultiFormatReader();
  private isScannerStarted = false;

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.startScannerOnce();
    } else {
      console.log("Running on SSR → camera disabled");
    }
  }

  startScannerOnce() {
    if (this.isScannerStarted) return;
    this.isScannerStarted = true;

    this.scanner.decodeFromVideoDevice(
      null,
      this.video.nativeElement,
      (result, err) => {
        if (result) {
          this.qrResult = result.getText();
          this.scanner.reset();
        }
      }
    );
  }

  processQR() {
    alert("Scanned: " + this.qrResult);
    this.router.navigate(['/send-money'], {
      state: { qr: this.qrResult }
    });
  }

  ngOnDestroy(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.scanner.reset();
    }
  }
}
