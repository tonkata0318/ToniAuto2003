using Microsoft.AspNetCore.Mvc;

namespace ToniAuto2003.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
