using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName)
        {
            if (userName.Equals("masong"))
            {
                return Redirect("/File/Index");
            }

            return Json("error");
        }

    }
}