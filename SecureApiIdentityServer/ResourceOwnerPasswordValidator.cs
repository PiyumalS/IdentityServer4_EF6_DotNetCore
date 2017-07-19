using Business;
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
        public ResourceOwnerPasswordValidator(IUserManager _userManager) { }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            if (true)// Identity Methods to validate the user
            {
                context.Result = new GrantValidationResult("1", "password", null, "local", null);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);
        }
    }
}
