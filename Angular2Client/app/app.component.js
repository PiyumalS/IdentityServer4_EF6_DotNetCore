var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import { AuthenticationService } from './services/authentication.service';
var AppComponent = (function () {
    function AppComponent(authenticationService, router) {
        this.authenticationService = authenticationService;
        this.router = router;
        this.signedIn = this.authenticationService.isSignedIn();
        this.name = this.authenticationService.getUser()
            .map(function (user) { return (typeof user.FirstName !== 'undefined') ? user.FirstName : null; });
        this.isAdmin = this.authenticationService.getRoles()
            .map(function (roles) { return roles.indexOf("SuperAdmin") != -1; });
        this.authenticationService.startupTokenRefresh();
    }
    AppComponent.prototype.signout = function () {
        this.authenticationService.signout();
        this.router.navigate(['/home']);
    };
    return AppComponent;
}());
AppComponent = __decorate([
    Component({
        selector: 'app-component',
        templateUrl: 'app.component.html'
    }),
    __metadata("design:paramtypes", [AuthenticationService, Router])
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map