import { Component } from '@angular/core';
import { Customer } from '../model/customer.model';
import { HttpClient } from '@angular/common/http';
import { response } from 'express';
import { error } from 'console';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
customer:Customer = new Customer();
message : string ='';

constructor(private http:HttpClient){}

registerCustomer(){
  this.http.post('https://localhost:7110/api/Auth/register',this.customer)
  .subscribe({
    next:(response:any) =>{
        this.message = response.message ;
    },
    error:(error) =>{
      console.error(error);
      this.message ='Something went wrong';
    }
  })
}
}
