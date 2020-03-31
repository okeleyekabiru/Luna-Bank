import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from 'src/NavBar/NavbarComponet';
import { RouterModule } from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import { ApppageComponent } from './Apppage.component';
import { FooterComponent } from './Footer/footer.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { GetAllAccountComponent } from './account/getallaccount.component';
import { HttpClientModule } from '@angular/common/http';
import { AccountService } from './account/AccountServices';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ApppageComponent,
    FooterComponent,
    UserComponent,
    RegistrationComponent
    GetAllAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule,
    RouterModule.forRoot([
      // route for all component
      { path: 'welcome', component: ApppageComponent },
      { path: 'AllAccount', component: GetAllAccountComponent },
      // upon load it locates welcome route
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      // if the route doesnt match any route it loads welcome
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
  ],
  providers: [
    // account service that would hold all methods to be used in accounts component
    AccountService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
