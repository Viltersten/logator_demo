import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AdminService } from "../admin.service";

@Component({
  selector: "app-claim",
  templateUrl: "./claim.component.html",
  styleUrls: ["./claim.component.scss"]
})
export class ClaimComponent implements OnInit {

  constructor(private service: AdminService, private route: ActivatedRoute) { }

  submitted = false;
  failed = false;
  networkId = "";
  name = "";
  email = "";

  ngOnInit() {
    this.route.queryParams.subscribe(_ => {
      this.networkId = _.networkId;
      this.name = _.name;
      this.email = _.email;
    });
  }

  onClick() {
    this.service.claimNetwork(this.networkId, this.email)
      .subscribe(next => {
        this.submitted = true;
        this.failed = !next;
      });
  }
}
