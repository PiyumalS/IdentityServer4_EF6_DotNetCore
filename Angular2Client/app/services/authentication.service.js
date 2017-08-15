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
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/interval';
import 'rxjs/add/observable/timer';
import { AuthHttp } from 'angular2-jwt';
import { Config } from '../config';
var AuthenticationService = (function () {
    function AuthenticationService(http, authHttp) {
        this.http = http;
        this.authHttp = authHttp;
        this.signinSubject = new BehaviorSubject(this.tokenNotExpired());
        this.userSubject = new BehaviorSubject({});
        this.rolesSubject = new BehaviorSubject([]);
        this.offsetSeconds = 30;
        this.getUserInfo();
        this.headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        this.options = new RequestOptions({ headers: this.headers });
    }
    AuthenticationService.prototype.signin = function (username, password) {
        var _this = this;
        var tokenEndpoint = Config.TOKEN_ENDPOINT;
        var params = {
            client_id: Config.CLIENT_ID,
            grant_type: Config.GRANT_TYPE,
            username: username,
            password: password,
            scope: Config.SCOPE,
            client_secret: Config.CLIENT_SECRET
        };
        var body = this.encodeParams(params);
        this.authTime = new Date().valueOf();
        return this.http.post(tokenEndpoint, body, this.options)
            .map(function (res) {
            var body = res.json();
            if (typeof body.access_token !== 'undefined') {
                _this.store(body);
                _this.getUserInfo();
                _this.signinSubject.next(true);
            }
        }).catch(function (error) {
            return Observable.throw(error);
        });
    };
    AuthenticationService.prototype.scheduleRefresh = function () {
        var _this = this;
        var source = this.authHttp.tokenStream.flatMap(function (token) {
            var delay = _this.expiresIn - _this.offsetSeconds * 1000;
            return Observable.interval(delay);
        });
        this.refreshSubscription = source.subscribe(function () {
            _this.getNewToken().subscribe(function () {
            }, function (error) {
                console.log(error);
            });
        });
    };
    AuthenticationService.prototype.startupTokenRefresh = function () {
        var _this = this;
        if (this.signinSubject.getValue()) {
            var source = this.authHttp.tokenStream.flatMap(function (token) {
                var now = new Date().valueOf();
                var exp = Helpers.getExp();
                var delay = exp - now - _this.offsetSeconds * 1000;
                return Observable.timer(delay);
            });
            source.subscribe(function () {
                _this.getNewToken().subscribe(function () {
                    _this.scheduleRefresh();
                }, function (error) {
                    console.log(error);
                });
            });
        }
    };
    AuthenticationService.prototype.unscheduleRefresh = function () {
        if (this.refreshSubscription) {
            this.refreshSubscription.unsubscribe();
        }
    };
    AuthenticationService.prototype.getNewToken = function () {
        var _this = this;
        var refreshToken = Helpers.getToken('refresh_token');
        var tokenEndpoint = Config.TOKEN_ENDPOINT;
        var params = {
            client_id: Config.CLIENT_ID,
            grant_type: "refresh_token",
            refresh_token: refreshToken,
            client_secret: Config.CLIENT_SECRET
        };
        var body = this.encodeParams(params);
        this.authTime = new Date().valueOf();
        return this.http.post(tokenEndpoint, body, this.options)
            .map(function (res) {
            var body = res.json();
            if (typeof body.access_token !== 'undefined') {
                _this.store(body);
            }
        }).catch(function (error) {
            return Observable.throw(error);
        });
    };
    AuthenticationService.prototype.revokeToken = function () {
        Helpers.removeToken('id_token');
        Helpers.removeExp();
    };
    AuthenticationService.prototype.revokeRefreshToken = function () {
        var refreshToken = Helpers.getToken('refresh_token');
        if (refreshToken != null) {
            var revocationEndpoint = Config.REVOCATION_ENDPOINT;
            var params = {
                client_id: Config.CLIENT_ID,
                token_type_hint: "refresh_token",
                token: refreshToken,
                client_secret: Config.CLIENT_SECRET
            };
            var body = this.encodeParams(params);
            this.http.post(revocationEndpoint, body, this.options)
                .subscribe(function () {
                Helpers.removeToken('refresh_token');
            });
        }
    };
    AuthenticationService.prototype.signout = function () {
        this.redirectUrl = null;
        this.signinSubject.next(false);
        this.userSubject.next({});
        this.rolesSubject.next([]);
        this.unscheduleRefresh();
        this.revokeToken();
        this.revokeRefreshToken();
    };
    AuthenticationService.prototype.isSignedIn = function () {
        return this.signinSubject.asObservable();
    };
    AuthenticationService.prototype.getUser = function () {
        return this.userSubject.asObservable();
    };
    AuthenticationService.prototype.getRoles = function () {
        return this.rolesSubject.asObservable();
    };
    AuthenticationService.prototype.tokenNotExpired = function () {
        var token = Helpers.getToken('id_token');
        return token != null && (Helpers.getExp() > new Date().valueOf());
    };
    AuthenticationService.prototype.getUserInfo = function () {
        var _this = this;
        if (this.tokenNotExpired()) {
            this.authHttp.get(Config.USERINFO_ENDPOINT)
                .subscribe(function (res) {
                var user = res.json();
                var roles = user.role;
                _this.userSubject.next(user);
                _this.rolesSubject.next(user.role);
            }, function (error) {
                console.log(error);
            });
        }
    };
    AuthenticationService.prototype.encodeParams = function (params) {
        var body = "";
        for (var key in params) {
            if (body.length) {
                body += "&";
            }
            body += key + "=";
            body += encodeURIComponent(params[key]);
        }
        return body;
    };
    AuthenticationService.prototype.store = function (body) {
        Helpers.setToken('id_token', body.access_token);
        Helpers.setToken('refresh_token', body.refresh_token);
        this.expiresIn = body.expires_in * 1000;
        Helpers.setExp(this.authTime + this.expiresIn);
    };
    return AuthenticationService;
}());
AuthenticationService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [Http, AuthHttp])
], AuthenticationService);
export { AuthenticationService };
var Helpers = (function () {
    function Helpers() {
    }
    Helpers.getToken = function (name) {
        return localStorage.getItem(name);
    };
    Helpers.setToken = function (name, value) {
        localStorage.setItem(name, value);
    };
    Helpers.removeToken = function (name) {
        localStorage.removeItem(name);
    };
    Helpers.setExp = function (exp) {
        localStorage.setItem("exp", exp.toString());
    };
    Helpers.getExp = function () {
        return parseInt(localStorage.getItem("exp"));
    };
    Helpers.removeExp = function () {
        localStorage.removeItem("exp");
    };
    return Helpers;
}());
//# sourceMappingURL=authentication.service.js.map