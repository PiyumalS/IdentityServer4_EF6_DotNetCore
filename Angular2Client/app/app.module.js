var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuard } from './services/auth.guard';
import { AuthenticationService } from './services/authentication.service';
import { IdentityService } from './services/identity.service';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ResourcesComponent } from './resources/resources.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SigninComponent } from './account/signin.component';
import { SignupComponent } from './account/signup.component';
import { MaterialModule } from './shared/material.module';
import { AuthHttp, AuthConfig } from 'angular2-jwt';
export function getAuthHttp(http) {
    return new AuthHttp(new AuthConfig({
        noJwtError: true,
        tokenGetter: (function () { return localStorage.getItem('id_token'); })
    }), http);
}
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    NgModule({
        imports: [
            BrowserModule,
            BrowserAnimationsModule,
            FormsModule,
            HttpModule,
            AppRoutingModule,
            MaterialModule
        ],
        declarations: [
            AppComponent,
            HomeComponent,
            ResourcesComponent,
            DashboardComponent,
            SigninComponent,
            SignupComponent
        ],
        providers: [
            AuthGuard,
            AuthenticationService,
            IdentityService,
            {
                provide: AuthHttp,
                useFactory: getAuthHttp,
                deps: [Http]
            },
            { provide: LocationStrategy, useClass: HashLocationStrategy }
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map