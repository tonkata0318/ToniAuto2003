using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Attributes;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Leasing;
using ToniAuto2003.Core.Services;
using ToniAuto2003.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class LeasingController : Controller
    {
        private readonly ILeasingService leasingService;
        private readonly IAgentService agentService;

        public LeasingController(ILeasingService _leasingservice, IAgentService _agentService)
        {
            leasingService = _leasingservice;
            agentService = _agentService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllLeasingQueryModel query)
        {
            var model = await leasingService.AllAsync(
                query.SearchTerm,
                query.Sorting,
                query.currentPage,
                query.LeasingsPerpage);

            query.TotalLeasingsCount = model.TotalLeasingsCount;
            query.Leasings = model.Leasings;
            return View(query);
        }

        [HttpGet]
        [MustBeAnAgentAtribute]
        public async Task<IActionResult> Add()
        {
            var model = new LeasingFormModel()
            {
            };
            return View(model);
        }
        [MustBeAnAgentAtribute]
        [HttpPost]
        public async Task<IActionResult> Add(LeasingFormModel car)
        {

            if (ModelState.IsValid == false)
            {
                return View(car);
            }

            int? agentId = await agentService.GetAgentIdAsync(User.Id());

            int newLeasingId = await leasingService.CreateAsync(car, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newLeasingId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new LeasingFormModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new LeasingFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LeasingFormModel model)
        {
            return RedirectToAction(nameof(Details), new { id = 1 });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new LeasingDetailsViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LeasingDetailsViewModel model)
        {
            return RedirectToAction(nameof(All));
        }
    }
}
