using Microsoft.AspNetCore.Authorization;

namespace Identity.Authorization.ClaimBasedAuthorization.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ClaimBasedAuthorizationAttribute : AuthorizeAttribute
    {
        public ClaimBasedAuthorizationAttribute(string claimToAuthorize) : base("fake")
        {
            ClaimToAuthorize = claimToAuthorize;
        }

        public string ClaimToAuthorize { get; }
    }
}
