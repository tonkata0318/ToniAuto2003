using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Contracts.Client;
using ToniAuto2003.Infrastructure;
using ToniAuto2003.Services.Client;
using ToniAuto2003.Services.Client.Models;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IClientInterface _clients;
        public UserController(IClientInterface clients)
        {
            _clients=clients;
        }

        public async Task<IActionResult> StartShopping()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StartShopping(ClientIndexModel model)
        {
            var userId = User.Id();

            if (await _clients.ExistById(userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _clients.Create(userId, model.amount);

            return RedirectToAction(nameof(CatalogueController.All), "Catalogue");
        }
    }
}
