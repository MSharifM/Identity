namespace Identity.Authorization.ClaimBasedAuthorization.Utilities
{
    public interface IClaimBasedAuthorizationUtilities
    {
        string GetClaimToAuthorize(HttpContent httpContent);
    }
}
