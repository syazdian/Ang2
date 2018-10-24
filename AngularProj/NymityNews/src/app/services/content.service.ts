import { Injectable } from "@angular/core";
import { AuthHttp } from "angular2-jwt";

import { RequestOptions, Headers, Http } from "@angular/http";
import { Route } from "@angular/router";
import { Observable } from "rxjs/Rx";
//import { Observable } from "rxjs";
import "rxjs/add/observable/of";
import "rxjs/add/operator/map";

@Injectable({
  providedIn: "root"
})
export class ContentService {
  constructor(private authhttp: AuthHttp, private http: Http) {}
  url: string = "http://localhost:43416/api/content/";

  CreateHeader(): any {
    let header = new Headers();
    let token = localStorage.getItem("token");
    header.append("Authorization", "Bearer " + token);
    let options = new RequestOptions({ headers: header });
    return options;
  }

  getAllGroups() {
    return this.http.get(this.url + "GetAllGroups", this.CreateHeader());
    // return this.authhttp.get(this.url + "GetAllGroups"); //.map(res => res.json());
  }

  getByGroupId(groupid: number) {
    return this.http.get(
      this.url + "GetByGroupId/" + groupid,
      this.CreateHeader()
    );
    // return this.authhttp.get(this.url + "GetByGroupId/" + groupid);
    //.map(res => res.json());
  }
}
