import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Console } from 'console';
import { HttpClient } from '@angular/common/http';
import { LoginDTO } from '../model/loginDTO.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
login :LoginDTO = new LoginDTO();
loading : boolean=false;

constructor(private http:HttpClient,private router:Router){}

  onLogin(){
    this.loading = true;
   this.http.post<any>('https://localhost:7110/api/Auth/login',this.login)
   .subscribe(
    {
      next:(response:any) =>{
        localStorage.setItem('token',response.token);
        localStorage.setItem('customer',JSON.stringify(response.customer))
        this.loading = true;
        alert("Login Successful");
        this.router.navigate(['/home']);

      },
      error:(error) =>{
        this.loading = false;
        console.error(error);
        alert('Invalid email or password');
      }
    }
   )
  }
}
