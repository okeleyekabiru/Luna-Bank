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

import { HttpClientModule } from '@angular/common/http';
import { AccountService } from './account/AccountServices';
import { GetAllAccountComponent } from './account/getallaccount.component';
import { LoginComponent } from './user/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ApppageComponent,
    FooterComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
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
