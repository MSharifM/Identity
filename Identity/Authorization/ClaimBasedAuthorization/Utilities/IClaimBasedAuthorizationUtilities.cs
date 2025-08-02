namespace Identity.Authorization.ClaimBasedAuthorization.Utilities
{
    public interface IClaimBasedAuthorizationUtilities
    {
        string GetClaimToAuthorize(HttpContext httpContext);
    }
}
