import { Component, OnInit } from "@angular/core";
import { AdminService } from "../admin.service";
import { Router, NavigationExtras } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {

  constructor(private service: AdminService, private router: Router) { }

  userName = "";
  password = "";

  ngOnInit() { }

  onClick() {
    this.service.logIn(this.userName, this.password)
      .subscribe(
        next => {
          const extras: NavigationExtras = { queryParams: { token: this.service.token } };
          if (next)
            this.router.navigate(["mapping/main"], extras);
          else
            this.router.navigate(["home"]);
        });
  }
}
