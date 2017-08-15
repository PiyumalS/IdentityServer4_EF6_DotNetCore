var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { AuthHttp } from 'angular2-jwt';
var IdentityService = (function () {
    function IdentityService(authHttp, http) {
        this.authHttp = authHttp;
        this.http = http;
        this.headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: this.headers });
    }
    IdentityService.prototype.GetAll = function () {
        return this.authHttp.get("http://localhost:59330/api/values")
            .map(function (res) {
            return res.json();
        })
            .catch(function (error) {
            return Observable.throw(error);
        });
    };
    return IdentityService;
}());
IdentityService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [AuthHttp, Http])
], IdentityService);
export { IdentityService };
//# sourceMappingURL=identity.service.js.map