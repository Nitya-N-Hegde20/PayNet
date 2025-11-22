import { Routes } from '@angular/router';
import { Register } from './register/register';
import { Home } from './home/home';
import { Login } from './login/login';
import { AddPhone } from './add-phone/add-phone';
import { Dashboard } from './dashboard/dashboard';
import { CreateAccount } from './create-account/create-account';

export const routes: Routes = [
    {path:'home', component:Home},
    {path:'login', component:Login},
    {path:'register', component:Register},
    {path:'addphone', component:AddPhone},
    {path:'dashboard', component:Dashboard},
    {path:'create-account', component:CreateAccount},
    {path:'',redirectTo:'home',pathMatch:'full'}
];
