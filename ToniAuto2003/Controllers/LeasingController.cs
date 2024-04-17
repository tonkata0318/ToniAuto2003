using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Attributes;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Leasing;
using ToniAuto2003.Core.Services;
using ToniAuto2003.Extensions;
using ToniAuto2003.Infrastructure.Data;
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
            if (await leasingService.ExistsAsync(id)==false)
            {
                return BadRequest();
            }
            else
            {
                var model=await leasingService.LeasingDetailsByIdAsync(id);

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await leasingService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await leasingService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            var model = await leasingService.GetLeasingFormModelByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LeasingServiceModel leasing)
        {
            if (await leasingService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await leasingService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid == false)
            {
                return View(leasing);
            }

            await leasingService.EditAsync(id, leasing);
            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await leasingService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await leasingService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            var leasing = await leasingService.LeasingDetailsByIdAsync(id);
            var model = new LeasingDetailsViewModel()
            {
                Id = id,
                Name=leasing.Name,
                AmountPerMonth=leasing.AmountPerMonth,
                Months=leasing.Months
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LeasingDetailsViewModel model)
        {
            if (await leasingService.ExistsAsync(model.Id) == false)
            {
                return BadRequest();
            }

            if (await leasingService.HasAgentWithIdAsync(model.Id, User.Id()) == false && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            await leasingService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
