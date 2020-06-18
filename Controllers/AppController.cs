using Microsoft.AspNetCore.Mvc;

namespace gestao.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}