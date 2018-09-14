import { Component, OnInit } from "@angular/core";
import { environment } from "../../../environments/environment";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {

  constructor() { }

  get distribution(): string { return "v. " + environment.version + " (" + (environment.production ? "prod" : "test") + ")"; }

  ngOnInit() { }

}
