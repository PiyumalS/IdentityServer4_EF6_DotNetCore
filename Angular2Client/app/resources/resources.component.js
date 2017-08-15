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
import { AuthHttp } from 'angular2-jwt';
var ResourcesComponent = (function () {
    function ResourcesComponent(authHttp) {
        var _this = this;
        this.authHttp = authHttp;
        this.authHttp.get("http://localhost:59330/api/values")
            .subscribe(function (res) {
            _this.values = res.json();
        }, function (error) {
            _this.error = error.statusText + ' : ' + error.status;
        });
    }
    return ResourcesComponent;
}());
ResourcesComponent = __decorate([
    Component({
        templateUrl: 'resources.component.html'
    }),
    __metadata("design:paramtypes", [AuthHttp])
], ResourcesComponent);
export { ResourcesComponent };
//# sourceMappingURL=resources.component.js.map