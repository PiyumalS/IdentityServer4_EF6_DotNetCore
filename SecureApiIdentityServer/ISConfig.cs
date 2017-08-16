using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SecureApiIdentityServer
{
    public class ISConfig
    {
        // Identity resources (used by UserInfo endpoint).
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };
        }

        // Api resources.
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1" ) {
                    UserClaims = { "role" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "resourceOwner",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = new List<string>
                    {
                      "http://localhost:58670"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                        "api1"
                    },
                    AccessTokenLifetime = 60,
                    AllowOfflineAccess = true // For refresh token.
                }
            };
        }
    }
}
