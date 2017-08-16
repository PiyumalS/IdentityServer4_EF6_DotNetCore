/**
 * Configuration data for the app, as in Config.cs.
 */
export class Config {

    /**
     * @see https://identityserver4.readthedocs.io/en/dev/endpoints/token.html
     */
    public static readonly TOKEN_ENDPOINT: string = "http://localhost:59330/connect/token";

    public static readonly REVOCATION_ENDPOINT: string = "http://localhost:59330/connect/revocation";

    /**
     * @see https://identityserver4.readthedocs.io/en/dev/endpoints/userinfo.html
     */
    public static readonly USERINFO_ENDPOINT: string = "http://localhost:59330/connect/userinfo";

    public static readonly CLIENT_ID: string = "resourceOwner";

    /**
     * Resource Owner Password Credential grant.
     */
    public static readonly GRANT_TYPE: string = "password";

    /**
     * The api1, refresh token (offline_access) & user info (openid profile roles).
     */
    public static readonly SCOPE: string = "api1 offline_access openid";

    public static readonly CLIENT_SECRET: string = "secret";

}
