var Config = (function () {
    function Config() {
    }
    return Config;
}());
export { Config };
Config.TOKEN_ENDPOINT = "http://localhost:59330/connect/token";
Config.REVOCATION_ENDPOINT = "http://localhost:59330/connect/revocation";
Config.USERINFO_ENDPOINT = "http://localhost:59330/connect/userinfo";
Config.CLIENT_ID = "resourceOwner";
Config.GRANT_TYPE = "password";
Config.SCOPE = "api1 offline_access openid";
Config.CLIENT_SECRET = "secret";
//# sourceMappingURL=config.js.map