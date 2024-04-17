using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Agent;
using ToniAuto2003.Core.Models.Client;
using ToniAuto2003.Extensions;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {

        private readonly IClientService clientService;

        public ClientController(IClientService _clientService)
        {
            clientService = _clientService;
        }

        [HttpGet]
        public async Task<IActionResult> StartShopping()
        {
            if (await clientService.ExistByIdAsync(User.Id()))
            {
                return BadRequest();
            }
            if (await clientService.ExistAsAgentAsync(User.Id()))
            {
                return BadRequest();
            }
            var model = new BecomeClientFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StartShopping(BecomeClientFormModel model)
        {
            if (await clientService.ExistByIdAsync(User.Id()))
            {
                return BadRequest();
            }
            if (await clientService.ExistAsAgentAsync(User.Id()))
            {
                return BadRequest();
            }

            if (ModelState.IsValid==false)
            {
                return View(model);
            }

            await clientService.CreateAsync(User.Id(), (double)model.Money);
            return RedirectToAction(nameof(CarController.All), "Car");
        }
    }
}
