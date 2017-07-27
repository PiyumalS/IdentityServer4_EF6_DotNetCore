using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SecureApiIdentityServer.AuthorizationHandler
{
    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute(string permission) : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] { new HasPermissionRequirement(permission) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAsyncResourceFilter, IAuthorizationRequirement
        {
            private readonly IAuthorizationService _authService;
            private readonly HasPermissionRequirement _requiredPermission;

            public RequiresPermissionAttributeImpl(IAuthorizationService authService, HasPermissionRequirement requiredPermission)
            {
                _authService = authService;
                _requiredPermission = requiredPermission;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                if (!await _authService.AuthorizeAsync(context.HttpContext.User, context.ActionDescriptor.ToString(), _requiredPermission))
                {
                    context.Result = new ChallengeResult();
                    context.HttpContext.Response.StatusCode = Convert.ToInt16(HttpStatusCode.Unauthorized);

                    return;
                }

                await next();
            }
        }
    }
}
