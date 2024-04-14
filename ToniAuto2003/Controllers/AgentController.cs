using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Agent;
using ToniAuto2003.Extensions;

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
        public async Task<IActionResult> Become()
        {
            if (await agentService.ExistByIdAsync(User.Id()))
            {
                return BadRequest();
            }

            if (await agentService.ExistAsClientAsync(User.Id()))
            {
                return BadRequest();
            }

            var model=new BecomeAgentFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            return RedirectToAction(nameof(CarController.All), "Car");
        }
    }
}
