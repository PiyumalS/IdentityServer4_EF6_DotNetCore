using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Extensions;
using System.Security.Claims;
using IdentityModel;

namespace SecureApiIdentityServer
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userID = context.Subject.GetSubjectId();

            // add user claims (permissions + user data) , roles here
            var claims = new List<Claim>
            {
               new Claim(JwtClaimTypes.Subject, userID.ToString()),
               new Claim(JwtClaimTypes.Email, "aa@gmail.com"),
               new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
               new Claim(JwtClaimTypes.Role,"Admin"),
               new Claim("permission" ,"permission1")
            };

            context.IssuedClaims = claims;

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true; // check with identiy user;

            return Task.FromResult(0);
        }
    }
}
