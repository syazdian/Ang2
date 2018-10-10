import { credentials } from "../models/credentials";

import { Http, Jsonp } from "@angular/http";
import { Injectable } from "@angular/core";
import { tokenNotExpired, JwtHelper } from "angular2-jwt";
import "rxjs/add/operator/map";

@Injectable()
export class AuthService {
  currentUser: any;

  constructor(private http: Http) {
    let token = localStorage.getItem("token");
    if (token) {
      let jwt = new JwtHelper();
      this.currentUser = jwt.decodeToken(token);
    }
  }
  //JSON.stringify(credentials)
  // postComment(comment: NewComment)
  // {
  // return this.http.post('http://localhost:3603/Api/News/', comment).map(res => res);
  // }

  url: string = "http://localhost:43416/api/auth/login/";
  postlogin(credentials: credentials) {
    return (
      this.http
        //   .post(this.url, JSON.stringify(credentials) )
        .post(this.url, credentials)

        // .map(res => {
        //   console.log(res);

        //   return true;
        // });

        .map(response => {
          let result = response.json();
          // console.log(result);
          if (result && result.token) {
            localStorage.setItem("token", result.token);

            let jwt = new JwtHelper();
            this.currentUser = jwt.decodeToken(localStorage.getItem("token"));
            return true;
          } else return false;
        })
    );
  }

  logoutAuth() {
    localStorage.removeItem("token");
    this.currentUser = null;
  }

  isLoggedIn() {
    return tokenNotExpired("token");
  }
}
