using Domain;
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

            var user = context.User;
            List<string> toBeCheckedPermissions = requirement.Permission.Split(',').ToList();

            if (user != null)
            {
                List<string> contextPermissions = user.FindAll(PermissionUtil.Permission).ToList().ConvertAll(x => x.Value);

                if (contextPermissions != null && contextPermissions.Count > 0 && toBeCheckedPermissions.Intersect(contextPermissions).Count() > 0)
                {
                    context.Succeed(requirement);

                }
            }

            return Task.FromResult(0);
        }
    }
}
