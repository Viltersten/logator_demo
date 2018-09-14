import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { NavModule } from "./nav/nav.module";
import { AdminModule } from "./admin/admin.module";
import { MappingModule } from "./mapping/mapping.module";

import { AppComponent } from "./app.component";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NavModule,
    AdminModule,
    MappingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
