using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Extensions;
using System.Security.Claims;
using IdentityModel;
using Business;
using Domain;

namespace SecureApiIdentityServer
{
    public class ProfileService : IProfileService
    {
        public IUserManager _userManager;

        public ProfileService(IUserManager userManager)
        {
            _userManager = userManager;
        }

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
            UserDTO searchDTO = new UserDTO();
            searchDTO.Id = context.Subject.GetSubjectId();

            var existingUser = _userManager.FindUserByID(searchDTO);

            if (existingUser != null)
            {
                context.IsActive = existingUser.ActiveStatus;
            }
            else
            {
                context.IsActive = false; // check with identiy user;
            }
            return Task.FromResult(0);
        }
    }
}
