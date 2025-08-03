using System.Collections.ObjectModel;

namespace Identity.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims
{
    public static class EmployeeControllerClaimValue
    {
        public const string EmployeeIndex = nameof(EmployeeIndex);
        public const string EmployeeIndexPersian = "صفحه اصلی مدیریت کارکنان";

        public const string EmployeeDetail = nameof(EmployeeDetail);
        public const string EmployeeDetailPersian = "جزییات کارکنان";

        public static ReadOnlyCollection<(string claimValueEnglish, string claimValuePersian)> AllClaimValues;

        static EmployeeControllerClaimValue()
        {
            AllClaimValues =
                MvcClaimValuesUtilities.GetPersianAndEnglishClaimValues(typeof(EmployeeControllerClaimValue))
                    .ToList()
                    .AsReadOnly();
        }
    }
}
