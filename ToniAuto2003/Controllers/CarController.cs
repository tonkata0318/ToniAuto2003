using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Runtime.CompilerServices;
using ToniAuto2003.Core.Models.Car;

namespace ToniAuto2003.Controllers
{
    public class CarController : BaseController
    {
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel model)
        {
            return RedirectToAction(nameof(Details), new { id=1 });
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
