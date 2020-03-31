import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { GetAllAccountComponent } from './account/getallaccount.component';


const routes: Routes = [
  // if the route doesnt match any route it loads welcome
  { path: "**", redirectTo: "welcome", pathMatch: "full" },
  { path: "", redirectTo: "/user/registration", pathMatch: "full" },
  {
    path: "user",
    component: UserComponent,
    children: [{ path: "registration", component: RegistrationComponent }]
  },
  { path: "AllAccount", component: GetAllAccountComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
