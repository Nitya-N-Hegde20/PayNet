import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Console } from 'console';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password = '';

  login(){
    console.log("Logging in with",this.username,this.password );
  }
}
