import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { MainMenuBarComponent } from "./nav/nav.module";
import {
  HomeComponent, LoginComponent, OrganizeComponent, ClaimComponent, ResetComponent,
  RegisterComponent, ErrorComponent
} from "./admin/admin.module";
import { MainComponent, UpdateComponent, ListComponent, ScheduleComponent } from "./mapping/mapping.module";

const routes: Routes = [
  { path: "act", component: MainMenuBarComponent, outlet: "nav" },
  { path: "", component: HomeComponent },
  { path: "home", component: HomeComponent },
  { path: "admin/home", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "admin/login", component: LoginComponent },
  { path: "organize", component: OrganizeComponent },
  { path: "admin/organize", component: OrganizeComponent },
  { path: "claim", component: ClaimComponent },
  { path: "admin/claim", component: ClaimComponent },
  { path: "reset", component: ResetComponent },
  { path: "admin/reset", component: ResetComponent },
  { path: "register", component: RegisterComponent },
  { path: "admin/register", component: RegisterComponent },
  { path: "error", component: ErrorComponent },
  { path: "admin/error", component: ErrorComponent },
  { path: "mapping/main", component: MainComponent },
  { path: "mapping/update", component: UpdateComponent },
  { path: "mapping/list", component: ListComponent },
  { path: "mapping/schedule", component: ScheduleComponent },
  { path: "**", component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
