import { Component } from '@angular/core';

import { AuthHttp } from 'angular2-jwt';

@Component({
    templateUrl: 'resources.component.html'
})
export class ResourcesComponent {

    values: any;
    error: string;

    constructor(private authHttp: AuthHttp) {
        // Sends an authenticated request.
        this.authHttp.get("http://localhost:59330/api/values")
            .subscribe(
            (res: any) => {
                this.values = res.json();
            },
            (error: any) => {
                this.error = error.statusText + ' : ' + error.status;
            });
    }

}
