import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { ActivatedRoute } from "@angular/router";

@Injectable()
export class AdminService {

  constructor(private client: HttpClient, private route: ActivatedRoute) {
    this.route.queryParams.subscribe(_ => {
      this.token = _.token || this.emptyToken;
      this.claims = JSON.parse(window.atob(this.token.split(".")[1]));
      this.behalfId = _.behalfId || "";
    });
  }

  token: string;
  claims: any;
  behalfId: string;
  status: number;
  reason: string;
  emptyToken = "." + window.btoa(JSON.stringify("{}")) + ".";
  get authorized(): boolean {
    const expiration = this.claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration"];

    const valid = new Date(expiration) > new Date();
    const proper = this.token && this.token.split(".").length === 3;

    return valid && proper;
  }

  createNetwork(name: string, email: string): Observable<boolean> {
    const headers = new HttpHeaders({ name: name, email: email });

    return new Observable(_ => {
      this.client.post(environment.API + "Admin/RequestNetwork", null, { headers: headers })
        .subscribe(
          next => _.next(true),
          error => _.next(false));
    });
  }

  claimNetwork(networkId: string, email: string): Observable<boolean> {
    const headers = new HttpHeaders({ networkId: networkId, email: email });

    return new Observable(_ => {
      this.client.post(environment.API + "Admin/ClaimNetwork", null, { headers: headers })
        .subscribe(
          next => _.next(true),
          error => _.next(false));
    });
  }

  resetPassword(email: string, password: string): Observable<boolean> {
    const headers = new HttpHeaders({ email: email, password: password });

    return new Observable(_ => {
      this.client.post(environment.API + "Admin/ResetPassword", null, { headers: headers })
        .subscribe(
          next => _.next(true),
          error => _.next(false));
    });
  }

  logIn(userName: string, password: string): Observable<boolean> {
    const headers = new HttpHeaders({ userName: userName, password: password });

    return new Observable(_ => {
      this.client.post(environment.API + "Admin/LogIn", null, { headers: headers })
        .subscribe(
          next => {
            this.token = next["token"];
            // environment.token = next["token"];
            this.status = 200;
            this.reason = "OK";
            _.next(true);
          },
          error => {
            this.token = error["token"];
            // environment.token = next["token"];
            this.status = error["status"];
            this.reason = error["statusText"];
            _.next(false);
          });
    });
  }

  logOut() {
    this.token = this.emptyToken;
    this.claims = {};
  }
}
