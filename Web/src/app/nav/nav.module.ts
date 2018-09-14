import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MainMenuBarComponent } from "./main-menu-bar/main-menu-bar.component";

export { MainMenuBarComponent } from "./main-menu-bar/main-menu-bar.component";

@NgModule({
  imports: [CommonModule],
  exports: [MainMenuBarComponent],
  declarations: [MainMenuBarComponent]
})
export class NavModule { }
