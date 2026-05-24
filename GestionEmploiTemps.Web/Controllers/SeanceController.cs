using Microsoft.AspNetCore.Mvc;

namespace GestionEmploiTemps.Web.Controllers
{
    public class SeanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
