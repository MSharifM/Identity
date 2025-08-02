using Identity.Authorization.ClaimBasedAuthorization.Utilities;
using Identity.Models;
using Identity.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Identity.Authorization
{
    public class ClaimBasedAuthorizationHandler : AuthorizationHandler<ClaimBasedAuthorizationRequirement>
    {
        private readonly SignInManager<CustomizeUser> _signInManager;
        private readonly IClaimBasedAuthorizationUtilities _utilities;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimBasedAuthorizationHandler(SignInManager<CustomizeUser> signInManager, IClaimBasedAuthorizationUtilities utilities, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _utilities = utilities;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationRequirement requirement)
        {
            var claimToAuthorize = _utilities.GetClaimToAuthorize(_httpContextAccessor.HttpContext);

            if (string.IsNullOrWhiteSpace(claimToAuthorize))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (!_signInManager.IsSignedIn(context.User)) return Task.CompletedTask;

            if (context.User.HasClaim(ClaimStore.UserAccess, claimToAuthorize))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
