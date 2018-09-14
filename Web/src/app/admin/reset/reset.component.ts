import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AdminService } from "../admin.service";

@Component({
  selector: "app-reset",
  templateUrl: "./reset.component.html",
  styleUrls: ["./reset.component.scss"]
})
export class ResetComponent implements OnInit {

  constructor(private service: AdminService, private route: ActivatedRoute) { }

  submitted = false;
  failed = false;
  email = "";
  password = "";

  ngOnInit() {
    this.route.queryParams.subscribe(_ => this.email = _.email);
  }

  onClick() {
    this.service.resetPassword(this.email, this.password)
      .subscribe(next => {
        this.submitted = true;
        this.failed = !next;
      });
  }
}
