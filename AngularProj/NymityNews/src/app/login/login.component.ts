import { credentials } from "./../models/credentials";
import { AuthService } from "./../services/auth.service";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  constructor(private router: Router, private authService: AuthService) {}
  ngOnInit() {}

  credential: credentials;
  loginForm(userpass: any) {
    //  console.log(userpass);

    this.credential = new credentials(userpass.email, userpass.password);

    // console.log(this.credential);
    this.authService.postlogin(this.credential).subscribe(result => {
      // console.log(result);
      if (result) this.router.navigate(["/content/1"]);
      else this.invalidLogin = true;
    });
  }
}
