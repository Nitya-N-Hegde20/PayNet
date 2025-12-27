import { Routes } from '@angular/router';
import { Register } from './register/register';
import { Home } from './home/home';
import { Login } from './login/login';
import { AddPhone } from './add-phone/add-phone';
import { Dashboard } from './dashboard/dashboard';
import { CreateAccount } from './create-account/create-account';
import { Profile } from './profile/profile';
import { QR } from './qr/qr';
import { SendMoney } from './send-money/send-money';
import { PayContact } from './pay-contact/pay-contact';

export const routes: Routes = [
    {path:'home', component:Home},
    {path:'login', component:Login},
    {path:'register', component:Register},
    {path:'addphone', component:AddPhone},
    {path:'dashboard', component:Dashboard},
    {path:'create-account', component:CreateAccount},
    {path:'profile', component:Profile},
    {
  path: 'send-money',
  component: SendMoney,
  data: { ssr: false }
},

    {path:'qr', component:QR},
    {
  path: 'pay-contact',
  loadComponent: () =>
    import('./pay-contact/pay-contact').then(m => m.PayContact),
  data: { ssr: false }
}
,
    {path:'',redirectTo:'home',pathMatch:'full'}
];
