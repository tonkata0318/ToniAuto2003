using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Runtime.CompilerServices;
using ToniAuto2003.Attributes;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Extensions;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Extensions;
using ToniAuto2003.Infrastructure.Data;

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

            if (await agentService.ExistByIdAsync(userId)||User.IsAdmin())
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
        public async Task<IActionResult> Details(int id,string information)
        {
            if (await carService.ExistsAsync(id)==false)
            {
                return BadRequest();
            }

            var model = await carService.CarDetailsByIdAsync(id);

            if (information!=model.GetInformation())
            {
                return BadRequest();
            }
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
                ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist");
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

            return RedirectToAction(nameof(Details), new { id = newCarId, information=car.GetInformation()});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await carService.ExistsAsync(id)==false)
            {
                return BadRequest();
            }

            if (await carService.HasAgentWithIdAsync(id,User.Id())== false
                && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            var model=await carService.GetCarFormModelByIdasync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarFormModel car)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasAgentWithIdAsync(id, User.Id()) == false
                && User.IsAdmin()==false)
            {
                return Unauthorized();
            }

            if (await carService.CategoryExistsAsync(car.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist");
            }

            if (ModelState.IsValid==false)
            {
               car.Categories=await carService.AllCategoriesAsync();
                car.Leasings = await carService.AllLeasingsAsync();

                return View(car);
            }

            await carService.EditAsync(id, car);
            return RedirectToAction(nameof(Details), new { id = id, information = car.GetInformation() });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasAgentWithIdAsync(id,User.Id())==false
                 && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            var car=await carService.CarDetailsByIdAsync(id);
            var model = new CarDetailsViewModel()
            {
                Id= id,
                Year=car.Year,
                Model=car.Model,
                Make=car.Make,
                ImageUrl=car.ImageUrl
                
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarDetailsViewModel model)
        {
            if (await carService.ExistsAsync(model.Id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasAgentWithIdAsync(model.Id, User.Id())==false
                 && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            await carService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await agentService.ExistByIdAsync(User.Id())
                 && User.IsAdmin() == false)
            {
                return Unauthorized();
            }

            if (await carService.IsBuyedAsync(id))
            {
                return BadRequest();
            }

            var client = await clientService.GetClientFormModelById(User.Id());
            var car = carService.GetCarFormModelByIdasync(id);
            if (User.IsAdmin())
            {
                await carService.BuyAsync(id, User.Id());
                return RedirectToAction(nameof(All));
            }
            else
            {
                if (client.Money >= ((double)car.Result.Price))
                {
                    await carService.BuyAsync(id, User.Id());
                    await clientService.BuyAsync(client.Id, ((double)car.Result.Price));

                    return RedirectToAction(nameof(All));
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Sell(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.IsBuyedByUserWithIdAsync(id,User.Id()) == false)
            {
                return Unauthorized();
            }
            var client = await clientService.GetClientFormModelById(User.Id());
            var car = carService.GetCarFormModelByIdasync(id);
            await carService.SellAsync(id);
            await clientService.SellAsync(client.Id, ((double)car.Result.Price));

            return RedirectToAction(nameof(All));
        }
    }
}
