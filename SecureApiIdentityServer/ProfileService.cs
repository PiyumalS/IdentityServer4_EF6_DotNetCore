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
            UserDTO usrDTO = new UserDTO();
            usrDTO.Id = context.Subject.GetSubjectId();

            var userDetails = _userManager.FindUserRolesPermissions(usrDTO).Result;

            var userObj = userDetails.Item1;
            var usrRoles = userDetails.Item2;
            var usrPermission = userDetails.Item3;

            // add user claims (permissions + user data) , roles here
            var claims = new List<Claim>
            {
               new Claim(JwtClaimTypes.Subject, context.Subject.GetSubjectId()),
               new Claim(JwtClaimTypes.Email, userObj.Email),
               new Claim(JwtClaimTypes.EmailVerified, userObj.EmailConfirmed.ToString(), ClaimValueTypes.Boolean),
               new Claim("FullName",String.IsNullOrEmpty(userObj.FullName)?userObj.UserName:userObj.FullName),
               new Claim("UserName",userObj.UserName),
               new Claim("PhoneNumber",userObj.PhoneNumber),
               new Claim("IsFirstAttempt",userObj.IsFirstAttempt.ToString(),ClaimValueTypes.Boolean),
               new Claim("IsTempararyPassword",userObj.IsTempararyPassword.ToString(),ClaimValueTypes.Boolean)
            };

            for (int i = 0; i < usrRoles.Length; i++)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, usrRoles.ElementAt(i)));
            }

            for (int i = 0; i < usrPermission.Length; i++)
            {
                claims.Add(new Claim(PermissionUtil.Permission, usrPermission.ElementAt(i)));
            }

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
