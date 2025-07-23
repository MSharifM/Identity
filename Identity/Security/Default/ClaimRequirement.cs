using Microsoft.AspNetCore.Authorization;

namespace Identity.Security.Default
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public ClaimRequirement(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public string ClaimType { get; }
        public string ClaimValue { get; set; }
    }

}
