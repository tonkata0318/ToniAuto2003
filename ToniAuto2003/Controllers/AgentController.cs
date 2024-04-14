using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Attributes;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Agent;
using ToniAuto2003.Extensions;
using static ToniAuto2003.Core.Constants.MessageConstants;

namespace ToniAuto2003.Controllers
{
    public class AgentController : BaseController
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService _agentService)
        {
            agentService = _agentService;
        }

        [HttpGet]
        [NotAnAgent]
        public async Task<IActionResult> Become()
        {
            if (await agentService.ExistAsClientAsync(User.Id()))
            {
                return BadRequest();
            }

            var model=new BecomeAgentFormModel();
            return View(model);
        }

        [HttpPost]
        [NotAnAgent]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            if (await agentService.ExistAsClientAsync(User.Id()))
            {
                return BadRequest();
            }

            if (await agentService.UserWithPhoneNumberExistsAsync(model.PhoneNumber)) 
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if (await agentService.UserHasCarsBuyedAsync(User.Id()))
            {
                ModelState.AddModelError("Error", HasCars);
            }

            if (ModelState.IsValid==false)
            {
                return View(model);
            }

            await agentService.CreateAsync(User.Id(),model.PhoneNumber);

            return RedirectToAction(nameof(CarController.All), "Car");
        }
    }
}
