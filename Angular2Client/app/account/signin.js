var Signin = (function () {
    function Signin(router, authenticationService) {
        this.router = router;
        this.authenticationService = authenticationService;
        this.model = {};
        this.errorMessage = "";
    }
    Signin.prototype.signin = function () {
        var _this = this;
        this.authenticationService.signin(this.model.username, this.model.password)
            .subscribe(function () {
            _this.authenticationService.scheduleRefresh();
            var redirect = _this.authenticationService.redirectUrl
                ? _this.authenticationService.redirectUrl
                : '/home';
            _this.router.navigate([redirect]);
        }, function (error) {
            if (error.body != "") {
                var body = error.json();
                switch (body.error) {
                    case "invalid_grant":
                        _this.errorMessage = "Invalid email or password";
                        break;
                    default:
                        _this.errorMessage = "Unexpected error. Try again";
                }
            }
            else {
                var errMsg = (error.message) ? error.message :
                    error.status ? error.status + " - " + error.statusText : 'Server error';
                console.log(errMsg);
                _this.errorMessage = "Server error. Try later.";
            }
        });
    };
    return Signin;
}());
export { Signin };
//# sourceMappingURL=signin.js.map