import { AuthGuard } from "./services/authguard.service";

import {
  AuthHttp,
  AUTH_PROVIDERS,
  provideAuth,
  AuthConfig
} from "angular2-jwt/angular2-jwt";
//import { OrderService } from "./services/order.service";
///import { AdminAuthGuard } from "./admin-auth-guard.service";
import { MockBackend } from "@angular/http/testing";
//import { fakeBackendProvider } from "./helpers/fake-backend";
import { AuthService } from "./services/auth.service";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule, Http, BaseRequestOptions } from "@angular/http";
import { RouterModule, Routes } from "@angular/router";

import { AppComponent } from "./app.component";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { ContentComponent } from "./content/content.component";
import { NotfoundComponent } from "./notfound/notfound.component";
import { ContentService } from "./services/content.service";

export function getAuthHttp(http) {
  return new AuthHttp(
    new AuthConfig({
      tokenName: "token"
    }),
    http
  );
}

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "home", component: HomeComponent },
  { path: "login", component: LoginComponent },
  {
    path: "content/:id",
    component: ContentComponent,
    canActivate: [AuthGuard]
  },
  { path: "**", component: NotfoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ContentComponent,
    NotfoundComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpModule,
    FormsModule
  ],
  providers: [
    ContentService,
    AuthService,
    AuthGuard,
    // AdminAuthGuard,
    AuthHttp,
    {
      provide: AuthHttp,
      useFactory: getAuthHttp,
      deps: [Http]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
