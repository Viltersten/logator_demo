import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { LoginComponent } from "./login/login.component";
import { ErrorComponent } from "./error/error.component";
import { OrganizeComponent } from "./organize/organize.component";
import { ClaimComponent } from "./claim/claim.component";
import { ResetComponent } from "./reset/reset.component";
import { AdminService } from "./admin.service";

export { HomeComponent } from "./home/home.component";
export { RegisterComponent } from "./register/register.component";
export { LoginComponent } from "./login/login.component";
export { ErrorComponent } from "./error/error.component";
export { OrganizeComponent } from "./organize/organize.component";
export { ClaimComponent } from "./claim/claim.component";
export { ResetComponent } from "./reset/reset.component";
export { AdminService } from "./admin.service";

@NgModule({
  imports: [CommonModule],
  // exports: [HomeComponent, LoginComponent, OrganizeComponent, ClaimComponent, ResetComponent, RegisterComponent, ErrorComponent],
  providers: [AdminService],
  declarations: [HomeComponent, LoginComponent, OrganizeComponent, ClaimComponent, ResetComponent, RegisterComponent, ErrorComponent]
})
export class AdminModule { }
