import { Component, OnInit } from "@angular/core";
import { ContentService } from "../services/content.service";
import { groups } from "../models/groups";
import { content } from "../models/content";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { AuthService } from "./../services/auth.service";
@Component({
  selector: "app-content",
  templateUrl: "./content.component.html",
  styleUrls: ["./content.component.css"],
  providers: [ContentService]
})
export class ContentComponent implements OnInit {
  constructor(
    private api: ContentService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) {}
  groups: groups[];
  contents: content[];
  groupdid: number;
  ngOnInit() {
    this.api.getAllGroups().subscribe(response => {
      this.groups = response.json();
    });

    this.route.paramMap.subscribe(params => {
      this.groupdid = +params.get("id");
      this.api.getByGroupId(this.groupdid).subscribe(res => {
        this.contents = res.json();
      });
    });
  }
  logout() {
    this.authService.logoutAuth();
    this.router.navigate(["login"]);
  }
}
