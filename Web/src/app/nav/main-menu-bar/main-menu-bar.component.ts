import { Component, OnInit } from "@angular/core";
import { Router, Event, NavigationEnd } from "@angular/router";
import { AdminService } from "../../admin/admin.module";

@Component({
  selector: "app-main-menu-bar",
  templateUrl: "./main-menu-bar.component.html",
  styleUrls: ["./main-menu-bar.component.scss"]
})
export class MainMenuBarComponent implements OnInit {

  constructor(private service: AdminService, private router: Router) { }

  shown: boolean;

  ngOnInit() {
    // todo Change queryParams to queryParamMap to update whole application!

    this.router.events.subscribe((_: Event) => {
      if (_ instanceof NavigationEnd)
        this.shown = _.url.startsWith("/mapping");
    });
  }

  onClick(target: string) {
    if (target === "logout")
      this.service.logOut();

    if (this.service.authorized)
      this.router.navigate(["mapping/" + target], { queryParamsHandling: "merge" });
    else
      this.router.navigate([""]);
  }
}
