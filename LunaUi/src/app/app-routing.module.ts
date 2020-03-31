import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { GetAllAccountComponent } from './account/getallaccount.component';
import { ApppageComponent } from './Apppage.component';
import { LoginComponent } from './user/login/login.component';


const routes: Routes = [
  
  //Home Route
  { path: "", redirectTo: "welcome", pathMatch: "full" },
  {path: "welcome", component: ApppageComponent},
  {
    path: "user",
    component: UserComponent,
    children: [{ path: "registration", component: RegistrationComponent },
    {path: "login", component: LoginComponent}]
  },
  { path: "AllAccount", component: GetAllAccountComponent } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
