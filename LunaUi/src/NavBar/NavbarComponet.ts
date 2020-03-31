import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';

@Component({
  templateUrl: "./NavbarComponent.html",
  selector: "pm-navbar"
})
export class NavbarComponent implements OnInit {
  Search: string;
    shouldLogOut = false;
    
    constructor(private router: Router) { }
    
    ngOnInit() {
        if (localStorage.getItem("access_token") != null) {
            this.shouldLogOut = true;
        }
    }
    
  onLogout() {
    localStorage.removeItem("access_token");
    this.router.navigate(["/user/login"]);
  }
}