import { Component, OnInit } from "@angular/core";
import { MappingService } from "../mapping.service";
import { environment } from "../../../environments/environment.prod";
import { Router, NavigationExtras } from "@angular/router";

@Component({
  selector: "app-list",
  templateUrl: "./list.component.html",
  styleUrls: ["./list.component.scss"]
})
export class ListComponent implements OnInit {

  constructor(private service: MappingService, private router: Router) {
    this.members = [];
  }

  members: any[];

  ngOnInit() {
    this.service.getMembers()
      .subscribe(next => this.members = next);
  }

  onClick(id: string) {
    const extras: NavigationExtras = {
      queryParams: { behalfId: id },
      queryParamsHandling: "merge"
    };
    this.router.navigate(["mapping/update"], extras);
  }
}
