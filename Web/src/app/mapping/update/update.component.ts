import { Component, OnInit } from "@angular/core";
import { MappingService } from "../mapping.service";
import { Subscriber } from "rxjs/Subscriber";
import { AdminService } from "../../admin/admin.module";
import { Router, NavigationExtras } from "@angular/router";
import { FormGroup, FormControl, FormBuilder } from "@angular/forms";

@Component({
  selector: "app-update",
  templateUrl: "./update.component.html",
  styleUrls: ["./update.component.scss"]
})
export class UpdateComponent implements OnInit {

  constructor(private service: MappingService, private admin: AdminService, private router: Router, private builder: FormBuilder) {
    this.formData = this.builder.group({
      firstName: new FormControl(),
      lastName: new FormControl(),
      location: new FormControl(),
      email: new FormControl(),
      phone: new FormControl()
    });
    this.service.getMember(admin.behalfId).subscribe(
      next => {
        this.formData.setValue({
          firstName: next["firstName"],
          lastName: next["lastName"],
          location: next["address"],
          email: next["email"],
          phone: next["phone"]
        });
      }
    );
  }

  locations = [];
  overhead = "";
  formData: FormGroup;

  ngOnInit() { }

  onType() {
    const entry = this.formData.value["location"];

    this.service.getAddresses(entry)
      .subscribe(next => {
        this.locations = next;
        this.overhead = "(+" + (this.locations.length - 1) + ")";
        if (this.locations.length < 2)
          this.overhead = "";
      });
  }

  onClick(event: Event) {
    event.preventDefault();
    // TODO Inquiry why GeocoderResult isn't recognized despite the import of @types/googlesmaps.

    this.service.updateUser(this.formData.value, this.admin.behalfId)
      .subscribe(next => {
        const extras: NavigationExtras = { queryParamsHandling: "merge" };
        this.router.navigate(["mapping/main"], extras);
      });
  }
}
