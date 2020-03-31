import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { DebitComponent } from './payment/debit.component';


const routes: Routes = [
  { path: "", redirectTo: "/user/registration", pathMatch: "full" },
  { path: "paymentTransaction", component: DebitComponent },
  {
    path: "user",
    component: UserComponent,
    children: [{ path: "registration", component: RegistrationComponent }]
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
