using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class ManageRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
