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

            var model = await PrepareEditUserAccessViewModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAccess(EditUserAccessViewModel model)
        {
            if (!await _userManager.Users.AnyAsync(u => u.Id == model.UserId)) return NotFound();

            if (ModelState.IsValid)
            {
                if (!IsEditUserAccessModelValid(model))
                {
                    model = await PrepareEditUserAccessViewModelAsync(model.UserId);
                    ModelState.AddModelError("", "مقدایر دسترسی معتبر نمیباشد، لطفا این صفحه را دوباره باز بکنید.");
                    return View(model);
                }

                await EditUserAccessInternalAsync(model);
                await _userManager.UpdateSecurityStampAsync(await _userManager.FindByIdAsync(model.UserId));
            }

            model = await PrepareEditUserAccessViewModelAsync(model.UserId);
            return View(model);
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

        private async Task EditUserAccessInternalAsync(EditUserAccessViewModel model)
        {
            var userClaims = await _dBContext.UserClaims
                .Where(c => c.UserId == model.UserId && c.ClaimType == ClaimStore.UserAccess)
                .ToListAsync();

            _dBContext.UserClaims.RemoveRange(userClaims);

            await _dBContext.UserClaims.AddRangeAsync(model.UserClaimValues.Where(c => c.IsSelected)
                 .Select(c => new IdentityUserClaim<string>
                 {
                     UserId = model.UserId,
                     ClaimType = ClaimStore.UserAccess,
                     ClaimValue = c.ClaimValue
                 }));

            await _dBContext.SaveChangesAsync();
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
