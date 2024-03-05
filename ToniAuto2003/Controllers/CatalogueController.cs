using Microsoft.AspNetCore.Mvc;

namespace ToniAuto2003.Controllers
{
    public class CatalogueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult All()
        {
            return View();
        }
    }
}
