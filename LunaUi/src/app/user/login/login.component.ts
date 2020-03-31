import { Component, OnInit } from "@angular/core";
import { UserService } from "src/app/shared/user.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: []
})
export class LoginComponent implements OnInit {
  formModel = {
    Email: "",
    Password: ""
  };

  constructor(
    private service: UserService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    if (localStorage.getItem("access_token") != null)
      this.router.navigateByUrl("/welcome");
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem("access_token", res.data.token);
        this.router.navigateByUrl("/welcome");
      },
      err => {
        if (err.status == 400)
          this.toastr.error(
            "Incorrect username or password.",
            "Authentication failed."
          );
        else console.log(err);
      }
    );
  }
}
