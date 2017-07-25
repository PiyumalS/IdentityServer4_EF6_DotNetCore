using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureApiIdentityServer.AuthorizationHandler
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public HasPermissionRequirement(string permission)
        {
            this.Permission = permission;

        }
    }


    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            // Find the required permissions claim
            bool isOk = false;

            var user = context.User;


            var claim = context.User.FindFirst(
                c => c.Type == "permission"
                && c.Value == requirement.Permission);

            // Succeed if the permissions array contains the required permission
            if (claim != null)
                context.Succeed(requirement);


            return Task.FromResult(0);
        }
    }
}
