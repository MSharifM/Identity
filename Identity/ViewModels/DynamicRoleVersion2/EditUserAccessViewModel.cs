using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.DynamicRoleVersion2
{
    public class EditUserAccessViewModel
    {
        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        public List<EditUserAccessClaimValuesDto> UserClaimValues { get; set; }
    }

    public class EditUserAccessClaimValuesDto
    {
        [Required]
        public string ClaimValue { get; set; }
        public string ClaimValuePersian { get; set; }
        public bool IsSelected { get; set; }
    }
}
