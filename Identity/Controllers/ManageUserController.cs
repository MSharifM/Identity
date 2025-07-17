using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class ManageUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
