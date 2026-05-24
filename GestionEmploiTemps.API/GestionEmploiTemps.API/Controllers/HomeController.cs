using Microsoft.AspNetCore.Mvc;

namespace GestionEmploiTemps.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
