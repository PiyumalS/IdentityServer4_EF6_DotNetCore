﻿
import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/map';

import { LocalStoreManager } from './local-store-manager.service';
import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';
import { DBkeys } from './db-Keys';
import { JwtHelper } from './jwt-helper';
import { Utilities } from './utilities';
import { User } from '../models/user.model';
import { Permission, PermissionNames, PermissionValues } from '../models/permission.model';

@Injectable()
export class AuthService {

    public get loginUrl() { return this.configurations.loginUrl; }
    public get homeUrl() { return this.configurations.homeUrl; }

    public loginRedirectUrl: string;
    public logoutRedirectUrl: string;

    public reLoginDelegate: () => void;

    private previousIsLoggedInCheck = false;
    private _loginStatus = new Subject<boolean>();


    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: EndpointFactory, private localStorage: LocalStoreManager) {
        this.initializeLoginStatus();
    }


    private initializeLoginStatus() {
        this.localStorage.getInitEvent().subscribe(() => {
            this.reevaluateLoginStatus();
        });
    }


    gotoPage(page: string, preserveParams = true) {

        let navigationExtras: NavigationExtras = {
            preserveQueryParams: preserveParams, preserveFragment: preserveParams
        };


        this.router.navigate([page], navigationExtras);
    }


    redirectLoginUser() {
        let redirect = this.loginRedirectUrl && this.loginRedirectUrl != ConfigurationService.defaultHomeUrl ? this.loginRedirectUrl : this.homeUrl;
        this.loginRedirectUrl = null;


        let urlAndFragment = Utilities.splitInTwo(redirect, '#');

        let navigationExtras: NavigationExtras = {
            fragment: urlAndFragment.secondPart,
            preserveQueryParams: true
        };

        this.router.navigate([urlAndFragment.firstPart], navigationExtras);
    }


    redirectLogoutUser() {
        let redirect = this.logoutRedirectUrl ? this.logoutRedirectUrl : this.loginUrl;
        this.logoutRedirectUrl = null;

        this.router.navigate([redirect]);
    }


    redirectForLogin() {
        this.loginRedirectUrl = this.router.url;
        this.router.navigate([this.loginUrl]);
    }


    reLogin() {

        this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);

        if (this.reLoginDelegate) {
            this.reLoginDelegate();
        }
        else {
            this.redirectForLogin();
        }
    }

    login(userName: string, password: string, rememberMe?: boolean) {

        if (this.isLoggedIn)
            this.logout();

        return this.endpointFactory.getLoginEndpoint(userName, password)
            .map((response: Response) => this.processLoginResponse(response, rememberMe));
    }


    private processLoginResponse(response: Response, rememberMe: boolean) {

        let response_token = response.json();
        let accessToken = response_token.access_token;

        if (accessToken == null)
            throw new Error("Received accessToken was empty");

        let expiresIn: number = response_token.expires_in;

        let tokenExpiryDate = new Date();
        tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn);

        let accessTokenExpiry = tokenExpiryDate;

        let jwtHelper = new JwtHelper();
 
        let decodedIdToken = jwtHelper.decodeToken(response_token.access_token);

        let permissions: PermissionValues[] = Array.isArray(decodedIdToken.permission) ? decodedIdToken.permission : [decodedIdToken.permission];

        let user = new User(
            decodedIdToken.sub,
            decodedIdToken.name,
            decodedIdToken.fullname,
            decodedIdToken.email,
            decodedIdToken.jobTitle,
            decodedIdToken.phone,
            Array.isArray(decodedIdToken.role) ? decodedIdToken.role : [decodedIdToken.role]);
        user.isEnabled = true;

        this.saveUserDetails(user, permissions, accessToken, accessTokenExpiry, rememberMe);

        this.reevaluateLoginStatus(user);

        return user;
    }


    private saveUserDetails(user: User, permissions: PermissionValues[], accessToken: string, expiresIn: Date, rememberMe: boolean) {

        if (rememberMe) {
            this.localStorage.savePermanentData(accessToken, DBkeys.ACCESS_TOKEN);
            this.localStorage.savePermanentData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
            this.localStorage.savePermanentData(permissions, DBkeys.USER_PERMISSIONS);
            this.localStorage.savePermanentData(user, DBkeys.CURRENT_USER);
        }
        else {
            this.localStorage.saveSyncedSessionData(accessToken, DBkeys.ACCESS_TOKEN);
            this.localStorage.saveSyncedSessionData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
            this.localStorage.saveSyncedSessionData(permissions, DBkeys.USER_PERMISSIONS);
            this.localStorage.saveSyncedSessionData(user, DBkeys.CURRENT_USER);
        }

        this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
    }



    logout(): void {
        this.localStorage.deleteData(DBkeys.ACCESS_TOKEN);
        this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);
        this.localStorage.deleteData(DBkeys.USER_PERMISSIONS);
        this.localStorage.deleteData(DBkeys.CURRENT_USER);

        this.configurations.clearLocalChanges();

        this.reevaluateLoginStatus();
    }


    private reevaluateLoginStatus(currentUser?: User) {

        let user = currentUser || this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
        let isLoggedIn = user != null;

        if (this.previousIsLoggedInCheck != isLoggedIn) {
            setTimeout(() => {
                this._loginStatus.next(isLoggedIn);
            });
        }

        this.previousIsLoggedInCheck = isLoggedIn;
    }


    getLoginStatusEvent(): Observable<boolean> {
        return this._loginStatus.asObservable();
    }


    get currentUser(): User {

        let user = this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
        this.reevaluateLoginStatus(user);

        return user;
    }

    get userPermissions(): PermissionValues[] {
        return this.localStorage.getDataObject<PermissionValues[]>(DBkeys.USER_PERMISSIONS) || [];
    }

    get accessToken(): string {

        this.reevaluateLoginStatus();
        return this.localStorage.getData(DBkeys.ACCESS_TOKEN);
    }

    get accessTokenExpiryDate(): Date {

        this.reevaluateLoginStatus();
        return this.localStorage.getDataObject<Date>(DBkeys.TOKEN_EXPIRES_IN, true);
    }

    get isSessionExpired(): boolean {

        if (this.accessTokenExpiryDate == null) {
            return true;
        }

        return !(this.accessTokenExpiryDate.valueOf() > new Date().valueOf());
    }

    get isLoggedIn(): boolean {
        return this.currentUser != null;
    }

    get rememberMe(): boolean {
        return this.localStorage.getDataObject<boolean>(DBkeys.REMEMBER_ME) == true;
    }
}