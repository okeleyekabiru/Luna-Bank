import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from 'src/NavBar/NavbarComponet';
import { RouterModule } from '@angular/router';
import {FormsModule} from '@angular/forms'
import { ApppageComponent } from './Apppage.component';
import { FooterComponent } from './Footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ApppageComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'welcome', component:ApppageComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
  ],
  providers: [
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
