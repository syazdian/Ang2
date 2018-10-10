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
  constructor(private http: AuthHttp, private http1: Http) {}
  url: string = "http://localhost:43416/api/content/";

  getAllGroups() {
    // let header = new Headers();
    // let token = localStorage.getItem("token");
    // header.append("Authorization", "Bearer " + token);
    // let options = new RequestOptions({ headers: headers });

    //  return this.http1.get(this.url + "GetAllGroups", options);
    return this.http1.get(this.url + "GetAllGroups"); //.map(res => res.json());
  }

  getByGroupId(groupid: number) {
    return this.http1.get(this.url + "GetByGroupId/" + groupid);
    //.map(res => res.json());
  }
}
