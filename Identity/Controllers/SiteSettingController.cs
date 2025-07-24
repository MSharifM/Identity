using Humanizer;
using Identity.Models;
using Identity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Controllers
{
    public class SiteSettingController : Controller
    {
        private readonly AppDBContext _context;

        public SiteSettingController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.SiteSettings.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult RoleValidationGuid()
        {
            var roleValidationGuidSiteSetting = _context.SiteSettings.FirstOrDefault(t => t.Key == "RoleValidationGuid");
            var roleValidationGuid = roleValidationGuidSiteSetting?.Value;

            var model = new RoleValidationGuidViewModel()
            {
                Value = roleValidationGuid,
                LastTimeChanged = roleValidationGuidSiteSetting?.LastTimeChanged
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult RoleValidationGuid(RoleValidationGuidViewModel model)
        {
            var roleValidationGuidSiteSetting = _context.SiteSettings.FirstOrDefault(t => t.Key == "RoleValidationGuid");
            
            if(roleValidationGuidSiteSetting is null)
            {
                _context.SiteSettings.Add(new SiteSettings()
                {
                    Key = "RoleValidationGuid",
                    Value = Guid.NewGuid().ToString(),
                    LastTimeChanged = DateTime.Now
                });
            }
            else
            {
                roleValidationGuidSiteSetting.Value = Guid.NewGuid().ToString();
                roleValidationGuidSiteSetting.LastTimeChanged = DateTime.Now;
                _context.Update(roleValidationGuidSiteSetting);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
