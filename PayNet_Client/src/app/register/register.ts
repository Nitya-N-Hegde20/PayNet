import { Component } from '@angular/core';
import { Customer } from '../model/customer.model';
import { HttpClient } from '@angular/common/http';
import { response } from 'express';
import { error } from 'console';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
customer:Customer = new Customer();
message : string ='';

constructor(private http:HttpClient,private router: Router){}

registerCustomer(){
  this.http.post('https://localhost:7110/api/Auth/register',this.customer)
  .subscribe({
    next:(response:any) =>{
        this.message = response.message ;
        alert('Registration Successful');
        this.router.navigate(['/login']);
    },
    error:(error) =>{
      console.error(error);
      this.message ='Something went wrong';
    }
  })
}
}
