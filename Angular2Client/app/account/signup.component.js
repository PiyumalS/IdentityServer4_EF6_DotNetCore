var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
import { AuthenticationService } from '../services/authentication.service';
import { IdentityService } from '../services/identity.service';
import { Signin } from './signin';
var SignupComponent = (function (_super) {
    __extends(SignupComponent, _super);
    function SignupComponent(router, authenticationService, identityService) {
        var _this = _super.call(this, router, authenticationService) || this;
        _this.router = router;
        _this.authenticationService = authenticationService;
        _this.identityService = identityService;
        _this.errorMessages = [];
        return _this;
    }
    return SignupComponent;
}(Signin));
SignupComponent = __decorate([
    Component({
        templateUrl: 'signup.component.html'
    }),
    __metadata("design:paramtypes", [Router,
        AuthenticationService,
        IdentityService])
], SignupComponent);
export { SignupComponent };
//# sourceMappingURL=signup.component.js.map