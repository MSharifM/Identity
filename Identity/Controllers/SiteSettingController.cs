using Humanizer;
using Identity.Models;
using Identity.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Identity.Controllers
{
    public class SiteSettingController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMemoryCache _memoryCache;

        public SiteSettingController(AppDBContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
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
            _memoryCache.Remove("RoleValidationGuid");

            return RedirectToAction("Index");
        }
    }
}
