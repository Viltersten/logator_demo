import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { } from "@types/googlemaps";
import { AdminService } from "../admin/admin.module";

@Injectable()
export class MappingService {

  constructor(private service: AdminService, private client: HttpClient) { }

  getMember(id: string) {
    const headers = new HttpHeaders({ Authorization: "Bearer " + this.service.token });

    id = id || this.service.claims["MemberId"];

    return new Observable(_ => {
      this.client.get(environment.API + "Member/" + id, { headers: headers })
        .subscribe(next => _.next(next));
    });
  }

  getMembers(): Observable<any> {
    const headers = new HttpHeaders({ Authorization: "Bearer " + this.service.token });

    return new Observable(_ => {
      this.client.get(environment.API + "Member/all", { headers: headers })
        .subscribe(next => _.next(next));
    });
  }

  // setEmail(id: string, email: string): Observable<boolean> {
  //   const headers = new HttpHeaders({ Authorization: "Bearer " + this.token });

  //   return new Observable(_ => {
  //     this.client.post(environment.API + "Member/" + id, { email: email }, { headers: headers });
  //   });
  // }

  getAddresses(address: string): Observable<string[]> {
    return new Observable<string[]>(_ => {
      const coder = new google.maps.Geocoder()
        .geocode({ address: address }, (
          results: google.maps.GeocoderResult[],
          status: google.maps.GeocoderStatus) => {
          let output = [];
          if (status === google.maps.GeocoderStatus.OK)
            output = results.map(__ => __.formatted_address);
          _.next(output);
          _.complete();
        });
    });
  }

  dep_updateUser(
    address: string,
    phone: string,
    firstName: string,
    lastName: string,
    email: string = "",
    id: string = ""
  ): Observable<boolean> {
    return new Observable(_ => {
      const coder = new google.maps.Geocoder()
        .geocode({ address: address }, (
          results: google.maps.GeocoderResult[],
          status: google.maps.GeocoderStatus) => {
          const lat = results[0].geometry.location.lat();
          const lng = results[0].geometry.location.lng();

          address = results[0].formatted_address;
          let target = "other";
          if (!id) {
            id = this.service.claims["MemberId"];
            target = "self";
          }

          const headers = new HttpHeaders({
            Authorization: "Bearer " + this.service.token,
            "Content-Type": "application/json"
          });

          this.client.post(
            environment.API + "Member/" + target + "/" + id,
            { id: id, address: address, email: email, lat: lat, lng: lng, phone: phone, firstName: firstName, lastName: lastName },
            { headers: headers })
            .subscribe(
              next => _.next(true),
              error => _.next(false)
            );
        });
    });
  }

  updateUser(dto: any, id: string = ""): Observable<boolean> {
    console.log(dto);

    return new Observable(_ => {
      const coder = new google.maps.Geocoder()
        .geocode({ address: dto.location }, (
          results: google.maps.GeocoderResult[],
          status: google.maps.GeocoderStatus) => {
          const lat = results[0].geometry.location.lat();
          const lng = results[0].geometry.location.lng();

          dto.address = results[0].formatted_address;
          let target = "other";
          if (!id) {
            id = this.service.claims["MemberId"];
            target = "self";
          }

          const headers = new HttpHeaders({
            Authorization: "Bearer " + this.service.token,
            "Content-Type": "application/json"
          });

          dto.id = id;
          dto.lat = lat;
          dto.lng = lng;

          this.client.post(
            environment.API + "Member/" + target + "/" + id,
            dto,
            { headers: headers })
            .subscribe(
              next => _.next(true),
              error => _.next(false)
            );
        });
    });
  }
}



