import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../services/authentication.service';
import { IdentityService } from '../services/identity.service';
import { Signin } from './signin';

@Component({
    templateUrl: 'signup.component.html'
})
export class SignupComponent extends Signin {

    errorMessages: string[] = [];

    constructor(
        public router: Router,
        public authenticationService: AuthenticationService,
        private identityService: IdentityService
    ) {
        super(router, authenticationService);
    }

}
