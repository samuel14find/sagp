using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace gestao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Chefe do Setor")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }



}
