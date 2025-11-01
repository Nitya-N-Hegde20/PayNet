import { Routes } from '@angular/router';
import { Register } from './register/register';
import { Home } from './home/home';

export const routes: Routes = [
    {path:'home', component:Home},
    {path:'register', component:Register},
    {path:'',redirectTo:'home',pathMatch:'full'}
];
