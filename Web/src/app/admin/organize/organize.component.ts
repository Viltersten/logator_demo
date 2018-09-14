import { Component, OnInit } from "@angular/core";
import { AdminService } from "../admin.service";

@Component({
  selector: "app-organize",
  templateUrl: "./organize.component.html",
  styleUrls: ["./organize.component.scss"]
})
export class OrganizeComponent implements OnInit {

  constructor(private service: AdminService) { }

  submitted = false;
  failed = false;
  name = "";
  email = "";

  ngOnInit() { }

  onClick() {
    this.service.createNetwork(this.name, this.email)
      .subscribe(next => {
        this.submitted = true;
        this.failed = !next;
      });
  }
}
