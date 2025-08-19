using System.Collections.ObjectModel;

namespace Identity.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims
{
    public static class AllControllersClaimValues
    {
        public static readonly ReadOnlyCollection<(string claimValueEnglish, string claimValuePersian)> AllClaimValues;

        static AllControllersClaimValues()
        {
            var allClaimValues = new List<(string claimValueEnglish, string claimValuePersian)>();

            allClaimValues.AddRange(EmployeeControllerClaimValue.AllClaimValues);

            AllClaimValues = allClaimValues.AsReadOnly();
        }
    }
}
