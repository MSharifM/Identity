using Identity.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims;
using Identity.Models;
using Identity.Repositories;
using Identity.ViewModels.DynamicRoleVersion2;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class DynamicRoleVersion2Controller : Controller
    {
        private readonly UserManager<CustomizeUser> _userManager;
        private readonly AppDBContext _dBContext;

        public DynamicRoleVersion2Controller(UserManager<CustomizeUser> userManager, AppDBContext dBContext)
        {
            _userManager = userManager;
            _dBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> EditUserAccess(string id)
        {
            if (!await _userManager.Users.AnyAsync(u => u.Id == id))
                return NotFound();

            
            return View();
        }

        #region Helpers

        private async Task<EditUserAccessViewModel> PrepareEditUserAccessViewModelAsync(string userId)
        {
            var userName = await _userManager.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .SingleAsync();

            var userClaimValues = await _dBContext.UserClaims
                .Where(c => c.UserId == userId && c.ClaimType == ClaimStore.UserAccess)
                .Select(c => c.ClaimValue)
                .ToListAsync();

            var model = new EditUserAccessViewModel
            {
                UserId = userId,
                UserName = userName,
                UserClaimValues = AllControllersClaimValues.AllClaimValues
                    .Select(c => new EditUserAccessClaimValuesDto
                    {
                        ClaimValue = c.claimValueEnglish,
                        ClaimValuePersian = c.claimValuePersian,
                        IsSelected = userClaimValues.Contains(c.claimValueEnglish)
                    }).ToList()
            };

            return model;
        }

        private bool IsEditUserAccessModelValid(EditUserAccessViewModel model)
        {
            return model.UserClaimValues.All(c =>
                AllControllersClaimValues.AllClaimValues.Any(validClaim => 
                    validClaim.claimValueEnglish == c.ClaimValue));
        }

        #endregion
    }
}
