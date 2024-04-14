using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToniAuto2003.Core.Contracts.Car;
using ToniAuto2003.Core.Models.Home;
using ToniAuto2003.Models;

namespace ToniAuto2003.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService carService;

        public HomeController(
            ILogger<HomeController> logger,
            ICarService _carService
            )
        {
            _logger = logger;
            carService= _carService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await carService.LastThreeCars();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}