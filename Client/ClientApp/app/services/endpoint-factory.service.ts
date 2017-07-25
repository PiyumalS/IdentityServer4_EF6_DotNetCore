
import { Injectable, Injector } from '@angular/core';
import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/catch';

import { AuthService } from './auth.service';
import { ConfigurationService } from './configuration.service';


@Injectable()
export class EndpointFactory {

    static readonly apiVersion: string = "1";

    private readonly _loginUrl: string = "/connect/token";

    private get loginUrl() { return this.configurations.baseUrl + this._loginUrl; }



    private taskPauser: Subject<any>;
    private isRefreshingLogin: boolean;

    private _authService: AuthService;

    private get authService() {
        if (!this._authService)
            this._authService = this.injector.get(AuthService);

        return this._authService;
    }



    constructor(protected http: Http, protected configurations: ConfigurationService, private injector: Injector) {

    }


    getLoginEndpoint(userName: string, password: string): Observable<Response> {

        let header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");

        let searchParams = new URLSearchParams();
        searchParams.append('username', userName);
        searchParams.append('password', password);
        searchParams.append('grant_type', 'password');
        searchParams.append('client_id', 'resourceOwner');
        searchParams.append('scope', 'api1');
        searchParams.append('client_secret', 'secret');

        let requestBody = searchParams.toString();

        return this.http.post(this.loginUrl, requestBody, { headers: header });
    }

    protected getAuthHeader(includeJsonContentType?: boolean): RequestOptions {
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authService.accessToken });

        if (includeJsonContentType)
            headers.append("Content-Type", "application/json");

        return new RequestOptions({ headers: headers });
    }



    protected handleError(error, continuation: () => Observable<any>) {

        if (error.status == 401) {
            this.authService.reLogin();
            return Observable.throw('session expired');
        }

        if (error.url && error.url.toLowerCase().includes(this.loginUrl.toLowerCase())) {
            this.authService.reLogin();
            return Observable.throw('session expired');
        }
        else {
            return Observable.throw(error || 'server error');
        }
    }



    private pauseTask(continuation: () => Observable<any>) {
        if (!this.taskPauser)
            this.taskPauser = new Subject();

        return this.taskPauser.switchMap(continueOp => {
            return continueOp ? continuation() : Observable.throw('session expired');
        });
    }


    private resumeTasks(continueOp: boolean) {
        setTimeout(() => {
            if (this.taskPauser) {
                this.taskPauser.next(continueOp);
                this.taskPauser.complete();
                this.taskPauser = null;
            }
        });
    }
}