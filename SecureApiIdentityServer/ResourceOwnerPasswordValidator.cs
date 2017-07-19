using Business;
using Domain;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureApiIdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public IUserManager _userManager;
        public ResourceOwnerPasswordValidator(IUserManager userManager)
        {
            _userManager = userManager;

        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            UserDTO usrObj = new UserDTO();
            usrObj.NationalID = context.UserName;
            usrObj.Password = context.Password;


            var loginResult = _userManager.LoginAsync(usrObj).Result;

            if (loginResult.Item1)// Identity Methods to validate the user
            {
                context.Result = new GrantValidationResult(loginResult.Item2[0].ToString(), "password", null, "local", null);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, loginResult.Item2[0].ToString(), null);
            return Task.FromResult(context.Result);
        }
    }
}
