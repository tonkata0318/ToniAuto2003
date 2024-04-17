using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.Core.Contracts;

namespace ToniAuto2003.Controllers
{
    [Route("api/statistic")]
    [ApiController]
    public class StatisticApiController : ControllerBase
    {
        private readonly IStatisticService statisticService;

        public StatisticApiController(IStatisticService _statisticService)
        {
            statisticService = _statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistic()
        {
            var result=await statisticService.TotalAsync();

            return Ok(result);
        }
    }
}
