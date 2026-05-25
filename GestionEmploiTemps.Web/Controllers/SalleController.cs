using Microsoft.AspNetCore.Mvc;

namespace GestionEmploiTemps.Web.Controllers
{
    public class SalleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
