﻿import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './services/auth.guard';

import { HomeComponent } from './home/home.component';
import { ResourcesComponent } from './resources/resources.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SigninComponent } from './account/signin.component';
import { SignupComponent } from './account/signup.component';

// We use PathLocationStrategy - the default "HTML 5 pushState" style.
// https://angular.io/docs/ts/latest/guide/router.html#!#browser-url-styles
// Router on the server (see Startup.cs) must match the router on the client to use PathLocationStrategy.
const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'resources', component: ResourcesComponent, canActivate: [AuthGuard] },
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
    { path: 'signin', component: SigninComponent },
    { path: 'signup', component: SignupComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
