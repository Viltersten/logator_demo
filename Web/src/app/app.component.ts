import { Component } from "@angular/core";
import { MainMenuBarComponent } from "./nav/nav.module";
// import { MainMenuBarComponent } from "./nav/main-menu-bar/main-menu-bar.component";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  title = "Logator";
}
