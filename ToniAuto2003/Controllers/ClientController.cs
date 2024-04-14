using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Core.Models.Agent;
using ToniAuto2003.Core.Models.Client;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> StartShopping()
        {
            var model = new BecomeClientFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StartShopping(BecomeClientFormModel model)
        {
            return RedirectToAction(nameof(CarController.All), "Car");
        }
    }
}
