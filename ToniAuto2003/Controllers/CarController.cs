using Microsoft.AspNetCore.Mvc;

namespace ToniAuto2003.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
