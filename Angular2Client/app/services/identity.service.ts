import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { AuthHttp } from 'angular2-jwt';

/**
 * Identity service (to Identity Web API controller).
 */
@Injectable() export class IdentityService {

    headers: Headers;
    options: RequestOptions;

    constructor(private authHttp: AuthHttp, private http: Http) {
        // Creates header for post requests.
        this.headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: this.headers });
    }

    /**
     * Gets all values through AuthHttp.
     */
    public GetAll(): Observable<any> {
        // Sends an authenticated request.
        return this.authHttp.get("http://localhost:59330/api/values")
            .map((res: Response) => {
                return res.json();
            })
            .catch((error: any) => {
                return Observable.throw(error);
            });
    }

}
