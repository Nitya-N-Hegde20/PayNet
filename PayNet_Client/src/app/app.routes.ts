import { Routes } from '@angular/router';
import { Register } from './register/register';
import { Home } from './home/home';
import { Login } from './login/login';
import { AddPhone } from './add-phone/add-phone';

export const routes: Routes = [
    {path:'home', component:Home},
    {path:'login', component:Login},
    {path:'register', component:Register},
    {path:'addphone', component:AddPhone},
    {path:'',redirectTo:'home',pathMatch:'full'}
];
