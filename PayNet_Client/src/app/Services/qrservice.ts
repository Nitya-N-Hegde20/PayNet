import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class QRService {
   constructor(private http: HttpClient) {}

  getQRCode(customerId: number) {
    return this.http.get(`https://localhost:7110/api/QR/generate-qr/${customerId}`);
  }
}
