import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from 'src/NavBar/NavbarComponet';
import { RouterModule } from '@angular/router';
import {FormsModule} from '@angular/forms'
import { ApppageComponent } from './Apppage.component';
import { FooterComponent } from './Footer/footer.component';
import { GetAllAccountComponent } from './account/getallaccount.component';
import { HttpClientModule } from '@angular/common/http';
import { AccountService } from './account/AccountServices';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ApppageComponent,
    FooterComponent,
    GetAllAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'welcome', component: ApppageComponent },
      { path: 'AllAccount', component: GetAllAccountComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
  ],
  providers: [
    AccountService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
