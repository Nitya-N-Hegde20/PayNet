import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateAccountDTO } from '../model/createaccountDTO.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Account {
  private apiUrl = 'https://localhost:7110/api/Account';

  constructor(private http: HttpClient) {}

  createAccount(payload: CreateAccountDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}/create`, payload);
  }

  getBankList() {
  return this.http.get<any[]>('https://localhost:7110/api/Bank/list');
}
}
