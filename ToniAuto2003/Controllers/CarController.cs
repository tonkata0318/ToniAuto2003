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
        private readonly IClientService clientService;

        public CarController(ICarService _carService, IAgentService _agentService, IClientService _clientservice)
        {
            carService = _carService;
            agentService = _agentService;
            clientService = _clientservice;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllCarsQueryModel query)
        {   
            var model =await carService.AllAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.currentPage,
                query.CarsPerpage);

            query.TotalCarsCount = model.totalCarsCount;
            query.Cars = model.Cars;
            query.Categories=await carService.AllCategoriesNamesAsync();
            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();
            IEnumerable<CarServiceModel> model;

            if (await agentService.ExistByIdAsync(userId))
            {
                int agentId = await agentService.GetAgentIdAsync(userId) ?? 0;

                model = await carService.AllCarsByAgentIdAsync(agentId);
            }
            else
            {
                if (await clientService.ExistByIdAsync(userId))
                {
                    model = await carService.AllCarsByUserIdAsync(userId);
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await carService.ExistsAsync(id)==false)
            {
                return BadRequest();
            }
            var model = await carService.CarDetailsByIdAsync(id);

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
