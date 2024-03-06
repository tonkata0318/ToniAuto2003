using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Contracts.SalesMan;
using ToniAuto2003.Infrastructure;
using ToniAuto2003.Services.SalesMan.Model;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class SalesManController : Controller
    {
        private readonly ISalesManInterface _salesMans;

        public SalesManController(ISalesManInterface salesMans)
        {
            _salesMans = salesMans;
        }

        public IActionResult JoinUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> JoinUs(BecomeSalesManModel model)
        {
            var userId = User.Id();

            if (await _salesMans.ExistById(userId))
            {
                return BadRequest();
            }

            if (await _salesMans.SalesManWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exist enter another one");
            }

            if (await _salesMans.CheckIfHeIsClient(userId))
            {
                ModelState.AddModelError(nameof(userId),
                   "You are already a salesman stick to the team");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _salesMans.Create(userId, model.PhoneNumber);

            return RedirectToAction(nameof(CatalogueController.All), "Catalogue");
        }
    }
}
