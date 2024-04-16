using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Leasing;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class LeasingController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = new AllLeasingQueryModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var model = new LeasingDetailsViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LeasingFormModel model)
        {
            return RedirectToAction(nameof(Details), new { id = 1 });
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
