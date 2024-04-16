using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Runtime.CompilerServices;
using ToniAuto2003.Attributes;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Extensions;

namespace ToniAuto2003.Controllers
{
    public class CarController : BaseController
    {

        private readonly ICarService carService;
        private readonly IAgentService agentService;

        public CarController(ICarService _carService, IAgentService _agentService)
        {
            carService = _carService;
            agentService = _agentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {   
            var model = new AllCarsQueryModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var model = new AllCarsQueryModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var model = new CarDetailsViewModel();
            return View(model);
        }

        [HttpGet]
        [MustBeAnAgentAtribute]
        public async Task<IActionResult> Add()
        {
            var model = new CarFormModel()
            {
                Categories = await carService.AllCategoriesAsync(),
                Leasings = await carService.AllLeasingsAsync()
            };
            return View(model);
        }
        [MustBeAnAgentAtribute]
        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel car)
        {
            return RedirectToAction(nameof(Details), new { id = 1 });
            if (await carService.CategoryExistsAsync(car.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(car.CategoryId), "");
            }

            if (await carService.LeasingExistsAsync(car.LeasingId) == false)
            {
                ModelState.AddModelError(nameof(car.LeasingId), "");
            }

            if (ModelState.IsValid == false)
            {
                car.Categories = await carService.AllCategoriesAsync();
                car.Leasings = await carService.AllLeasingsAsync();

                return View(car);
            }

            int? agentId = await agentService.GetAgentIdAsync(User.Id());

            int newCarId = await carService.CreateAsync(car, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newCarId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model=new CarFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,CarFormModel model)
        {
            return RedirectToAction(nameof(Details), new { id = 1 });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new CarDetailsViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarDetailsViewModel model)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
        [HttpPost]
        public async Task<IActionResult> Sell(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
